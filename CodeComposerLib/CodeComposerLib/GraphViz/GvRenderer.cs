using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using TextComposerLib;

namespace CodeComposerLib.GraphViz
{
    /// <summary>
    /// This class represents a rendering method for GraphViz graphs.
    /// See "Layout Commands" under http://www.graphviz.org/Documentation.php for details
    /// </summary>
    public sealed class GvRenderer
    {
        /// <summary>
        /// dot draws directed graphs. It works well on directed acyclic graphs and other graphs that 
        /// can be drawn as hierarchies or have a natural "flow."
        /// </summary>
        public static readonly GvRenderer Dot 
            = new GvRenderer("dot", true, false);

        /// <summary>
        /// neato draws undirected graphs using a "spring" model and reducing the related energy
        /// </summary>
        public static readonly GvRenderer Neato 
            = new GvRenderer("neato", false, true);

        /// <summary>
        /// twopi draws graphs using a radial layout.
        /// Basically, one node is chosen as the center and put at the origin. The remaining nodes are placed on
        /// a sequence of concentric circles centered about the origin, each a fixed radial distance from 
        /// the previous circle. All nodes distance 1 from the center are placed on the first circle; 
        /// all nodes distance 1 from a node on the first circle are placed on the second circle; and so forth.
        /// </summary>
        public static readonly GvRenderer TwoPi 
            = new GvRenderer("twopi", true, true);

        /// <summary>
        /// circo draws graphs using a circular layout.
        /// The tool identifies biconnected components and draws the nodes of the component on a circle.
        /// The block-cutpoint tree is then laid out using a recursive radial algorithm. Edge crossings 
        /// within a circle are minimized by placing as many edges on the circle’s perimeter as possible. 
        /// In particular, if the component is outerplanar, the component will have a planar layout. 
        /// If a node belongs to multiple non-trivial biconnected components, the layout puts the node in 
        /// one of them. By default, this is the first non-trivial component found in the search from the 
        /// root component.
        /// </summary>
        public static readonly GvRenderer Circo 
            = new GvRenderer("circo", true, true);

        /// <summary>
        /// fdp draws undirected graphs using a "spring" model. It relies on a force-directed approach.
        /// </summary>
        public static readonly GvRenderer Fdp 
            = new GvRenderer("fdp", false, true);

        /// <summary>
        /// sfdp also draws undirected graphs using the "spring" model like fdp, but it uses a multi-scale
        /// approach to produce layouts of large graphs in a reasonably short time.
        /// </summary>
        public static readonly GvRenderer SFdp 
            = new GvRenderer("sfdp", false, true);

        /// <summary>
        /// patchwork draws the graph as a squarified treemap. The clusters of the graph are used
        /// to specify the tree.
        /// </summary>
        public static readonly GvRenderer PatchWork 
            = new GvRenderer("patchwork", true, true);

        /// <summary>
        /// osage draws the graph using its cluster structure. For a given cluster, each of its subclusters 
        /// is laid out internally. Then the subclusters, plus any remaining nodes, are repositioned based 
        /// on the cluster’s pack and packmode attributes.
        /// </summary>
        public static readonly GvRenderer OSage 
            = new GvRenderer("osage", true, true);


        ///// <summary>
        ///// ImageConverter object used to convert byte arrays containing JPEG or PNG file images into 
        ///// Bitmap objects. This is static and only gets instantiated once.
        ///// </summary>
        //private static ImageConverter ImageConverter { get; } 
        //    = new ImageConverter();

        /// <summary>
        /// Method that uses the ImageConverter object in .Net Framework to convert a byte array, 
        /// presumably containing a JPEG or PNG file image, into a Bitmap object, which can also be 
        /// used as an Image object.
        /// </summary>
        /// <param name="byteArray">byte array containing JPEG or PNG file image or similar</param>
        /// <returns>Bitmap object if it works, else exception is thrown</returns>
        private static Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            var tc = TypeDescriptor.GetConverter(typeof(Bitmap));

            var bm = (Bitmap)tc.ConvertFrom(byteArray);

            //var bm = (Bitmap)ImageConverter.ConvertFrom(byteArray);

            if (bm != null && (
                bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                bm.VerticalResolution != (int)bm.VerticalResolution
            ))
            {
                // Correct a strange glitch that has been observed in the test program when converting 
                //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
                //  slightly away from the nominal integer value
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm;
        }

        /// <summary>
        /// Read a byte array from a given binary stream
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] ReadFullStream(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);

                return ms.ToArray();
            }
        }


        private readonly List<string> _errorsList = new List<string>();

        private string _graphVizBinFolder = @"C:\Program Files (x86)\Graphviz2.38\bin\";


        /// <summary>
        /// Get or set a flag to allow for more detailed output log
        /// </summary>
        public bool VerboseOutput { get; set; }

        /// <summary>
        /// The name of this rendering method
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// True if this rendering method can be used for directed graphs
        /// </summary>
        public bool AllowDirectedGraphs { get; private set; }

        /// <summary>
        /// True if this rendering method can be used for undirected graphs
        /// </summary>
        public bool AllowUndirectedGraphs { get; private set; }

        /// <summary>
        /// Get or set the executable bin folder of GraphViz
        /// </summary>
        public string GraphVizBinFolder
        {
            get { return _graphVizBinFolder; }
            set
            {
                if (Directory.Exists(value))
                    _graphVizBinFolder = value;

                else
                    throw new DirectoryNotFoundException(value);
            }
        }

        /// <summary>
        /// True if the rendering process resulted in an error
        /// </summary>
        public bool HasRenderingErrors => _errorsList.Count > 0;

        /// <summary>
        /// A list of rendering errors
        /// </summary>
        public IEnumerable<string> RenderingErrors => _errorsList;

        /// <summary>
        /// A single string containing all rendering errors
        /// </summary>
        public string RenderingErrorsMessage
        {
            get
            {
                var s = new StringBuilder();

                foreach (var msg in _errorsList)
                    s.AppendLine(msg);

                return s.ToString();
            }
        }


        internal GvRenderer(string name, bool allowDirectedGraphs, bool allowUndirectedGraphs)
        {
            Name = name;
            AllowDirectedGraphs = allowDirectedGraphs;
            AllowUndirectedGraphs = allowUndirectedGraphs;
        }


        /// <summary>
        /// Construct the command line arguments of GraphViz rendering method
        /// </summary>
        /// <param name="returnType"></param>
        /// <returns></returns>
        private string GetArguments(string returnType)
        {
            return VerboseOutput
                ? "-v -o -T" + returnType
                : "-o -T" + returnType;
        }

        /// <summary>
        /// Create a ProcessStartInfo object for GraphViz rendering method
        /// </summary>
        /// <param name="returnType"></param>
        /// <returns></returns>
        private ProcessStartInfo GetProcessStartInfo(string returnType)
        {
            var filePath = Path.Combine(GraphVizBinFolder, Name + ".exe");

            return
                new ProcessStartInfo()
                {
                    WorkingDirectory = Path.GetDirectoryName(filePath) ?? "",
                    FileName = filePath.ValueToQuotedLiteral(),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    Arguments = GetArguments(returnType),
                    CreateNoWindow = true
                };
        }

        /// <summary>
        /// This is the error data handler for the rendering process
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="errLine"></param>
        private void ErrorDataHandler(object sendingProcess, DataReceivedEventArgs errLine)
        {
            if (String.IsNullOrEmpty(errLine.Data)) return;

            _errorsList.Add(errLine.Data);
        }

        /// <summary>
        /// Render the given GraphViz code into an image
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public byte[] RenderGraph(string dotSourceText, GvOutputFormat returnType)
        {
            _errorsList.Clear();

            byte[] output = null;

            var processStartInfo = GetProcessStartInfo(returnType.Name);

            using (var process = Process.Start(processStartInfo))
                if (ReferenceEquals(process, null) == false)
                {
                    process.ErrorDataReceived += ErrorDataHandler;

                    process.BeginErrorReadLine();

                    using (var stdIn = process.StandardInput)
                    {
                        stdIn.WriteLine(dotSourceText);
                    }

                    using (var stdOut = process.StandardOutput)
                    {
                        output = ReadFullStream(stdOut.BaseStream);
                    }
                }

            return output;
        }

        /// <summary>
        /// Render the given GraphViz code into a string. Some rendering methods output
        /// text rather than images
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public string RenderToText(string dotSourceText, GvTextOutputFormat returnType)
        {
            var byteArray = RenderGraph(dotSourceText, returnType);

            return byteArray.Length == 0 
                ? String.Empty 
                : Encoding.Default.GetString(byteArray);
        }

        /// <summary>
        /// Render the given GraphViz code into a string and also save it to file. 
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string RenderToTextAndFile(string dotSourceText, GvTextOutputFormat returnType, string filePath)
        {
            var byteArray = RenderGraph(dotSourceText, returnType);

            if (byteArray.Length == 0)
                return String.Empty;

            try
            {
                File.WriteAllBytes(filePath, byteArray);
            }
            catch (Exception e)
            {
                _errorsList.Add(e.Message);
            }

            return Encoding.Default.GetString(byteArray);
        }

        /// <summary>
        /// Render the given GraphViz code into an image. 
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public Image RenderToImage(string dotSourceText, GvImageOutputFormat returnType)
        {
            var byteArray = RenderGraph(dotSourceText, returnType);

            return byteArray.Length == 0 
                ? null 
                : GetImageFromByteArray(byteArray);

            //using (var stream = new MemoryStream(bytesArray))
            //{
            //    return Image.FromStream(stream);
            //}
        }

        /// <summary>
        /// Render the given GraphViz code into an image and also save it to file. 
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Image RenderToImageAndFile(string dotSourceText, GvImageOutputFormat returnType, string filePath)
        {
            var byteArray = RenderGraph(dotSourceText, returnType);

            if (byteArray.Length == 0)
                return null;

            try
            {
                File.WriteAllBytes(filePath, byteArray);
            }
            catch (Exception e)
            {
                _errorsList.Add(e.Message);
            }

            return GetImageFromByteArray(byteArray);
        }

        /// <summary>
        /// Render the given GraphViz code and save it to file. 
        /// </summary>
        /// <param name="dotSourceText"></param>
        /// <param name="returnType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool RenderToFile(string dotSourceText, GvOutputFormat returnType, string filePath)
        {
            var byteArray = RenderGraph(dotSourceText, returnType);

            if (byteArray.Length == 0)
                return false;

            try
            {
                File.WriteAllBytes(filePath, byteArray);
            }
            catch (Exception e)
            {
                _errorsList.Add(e.Message);
                return false;
            }
            
            return true;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
