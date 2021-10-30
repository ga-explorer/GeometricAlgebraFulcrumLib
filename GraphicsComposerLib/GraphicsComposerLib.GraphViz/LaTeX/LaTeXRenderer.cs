using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using GraphicsComposerLib.GraphViz.LaTeX.Templates;
using ImageMagick;
using TextComposerLib;
using TextComposerLib.Text;

namespace GraphicsComposerLib.GraphViz.LaTeX
{
    public class LaTeXRenderer
    {
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
        private string _workingPath = "";
        private string _laTeXPdfBinFolder = @"C:\miktex-portable\texmfs\install\miktex\bin";


        public string WorkingPath 
        {
            get => _workingPath ?? string.Empty;
            set
            {
                if (string.IsNullOrEmpty(value) || Directory.Exists(value))
                    _workingPath = value;

                else
                    throw new DirectoryNotFoundException(value);
            }
        }

        //public string InputFilePath
        //    => Path.Combine(_workingPath, "input.tex");

        //public string OutputFilePath
        //    => Path.Combine(_workingPath, "output.png");

        /// <summary>
        /// Get or set the executable bin folder of latexpdf.exe
        /// </summary>
        public string LaTeXPdfBinFolder
        {
            get => _laTeXPdfBinFolder;
            set
            {
                if (Directory.Exists(value))
                    _laTeXPdfBinFolder = value;

                else
                    throw new DirectoryNotFoundException(value);
            }
        }

        public string LaTeXPdfFilePath
            => Path.Combine(LaTeXPdfBinFolder, "pdflatex.exe");

        /// <summary>
        /// True if the rendering process resulted in an error
        /// </summary>
        public bool HasRenderingErrors 
            => _errorsList.Count > 0;

        /// <summary>
        /// A list of rendering errors
        /// </summary>
        public IEnumerable<string> RenderingErrors 
            => _errorsList;

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

        
        /// <summary>
        /// Construct the command line arguments of GraphViz rendering method
        /// </summary>
        /// <returns></returns>
        private string GetLaTeXPdfArguments(string pdfFileName)
        {
            return $"-jobname={pdfFileName} {pdfFileName}";
            //return $"-jobname={pdfFileName}";
        }

        /// <summary>
        /// Create a ProcessStartInfo object for latexpdf.exe
        /// </summary>
        /// <returns></returns>
        private ProcessStartInfo GetLaTeXPdfProcessStartInfo(string workingPath, string pdfFileName)
        {
            return
                new ProcessStartInfo()
                {
                    WorkingDirectory = workingPath,
                    FileName = LaTeXPdfFilePath.ValueToQuotedLiteral(),
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    Arguments = GetLaTeXPdfArguments(pdfFileName),
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
            if (string.IsNullOrEmpty(errLine.Data)) 
                return;

            _errorsList.Add(errLine.Data);
        }

        public bool RenderMathToPdfFile(string fileName, string mathText)
        {
            _errorsList.Clear();

            var latexSourceCode = 
                LaTeXTemplates.MathPreview1.Replace(
                    "#contents#", 
                    "$" + mathText + "$"
                );

            var texFilePath = Path.Combine(WorkingPath, fileName) + ".tex";
            var auxFilePath = Path.Combine(WorkingPath, fileName) + ".aux";
            var logFilePath = Path.Combine(WorkingPath, fileName) + ".log";

            File.WriteAllText(texFilePath, latexSourceCode);

            var processStartInfo = GetLaTeXPdfProcessStartInfo(WorkingPath, fileName);
            using (var process = Process.Start(processStartInfo))
            {
                if (ReferenceEquals(process, null)) 
                    return false;

                process.ErrorDataReceived += ErrorDataHandler;
                process.BeginErrorReadLine();

                while (!process.HasExited) ;

                if (File.Exists(texFilePath))
                    File.Delete(texFilePath);

                if (File.Exists(auxFilePath))
                    File.Delete(auxFilePath);

                if (File.Exists(logFilePath))
                    File.Delete(logFilePath);

                if (HasRenderingErrors)
                    return false;

                return true;
            }
        }

        public bool RenderMathToPdfFile(string fileName, IEnumerable<string> mathTextList)
        {
            _errorsList.Clear();

            var contentsString = 
                mathTextList
                    .Select(s => $"${s.Trim()}$")
                    .Concatenate(" \\newpage{}" + Environment.NewLine);

            var latexSourceCode = 
                LaTeXTemplates.MathPreview1.Replace(
                    "#contents#", 
                    contentsString
                );

            var texFilePath = Path.Combine(WorkingPath, fileName) + ".tex";
            var auxFilePath = Path.Combine(WorkingPath, fileName) + ".aux";
            var logFilePath = Path.Combine(WorkingPath, fileName) + ".log";

            File.WriteAllText(texFilePath, latexSourceCode);

            var processStartInfo = GetLaTeXPdfProcessStartInfo(WorkingPath, fileName);
            using (var process = Process.Start(processStartInfo))
            {
                if (ReferenceEquals(process, null)) 
                    return false;

                process.ErrorDataReceived += ErrorDataHandler;
                process.BeginErrorReadLine();

                while (!process.HasExited) ;

                //if (File.Exists(texFilePath))
                  //  File.Delete(texFilePath);

                if (File.Exists(auxFilePath))
                    File.Delete(auxFilePath);

                if (File.Exists(logFilePath))
                    File.Delete(logFilePath);

                if (HasRenderingErrors)
                    return false;

                return true;
            }
        }

        public bool ConvertPdfFileToPngFile(string pdfFileName, string pngFileName, double resolution)
        {
            var settings = new MagickReadSettings
            {
                // Setting the density to 300 dpi will create an image with a better quality
                Density = new Density(resolution, resolution)
            };

            var pdfFilePath = Path.Combine(WorkingPath, pdfFileName) + ".pdf";
            var pngFilePath = Path.Combine(WorkingPath, pngFileName) + ".png";

            using (var images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(pdfFilePath, settings);

                //// Create new image that appends all the pages horizontally
                //using (var horizontal = images.AppendHorizontally())
                //{
                //    // Save result as a png
                //    horizontal.Write(pngFilePath);
                //}

                // Create new image that appends all the pages vertically
                using (var vertical = images.AppendVertically())
                {
                    // Save result as a png
                    vertical.Write(pngFilePath);
                }
            }

            return true;
        }

        public bool RenderMathToPngFile(string fileName, string latexMath, double resolution)
        {
            var pdfDone = RenderMathToPdfFile(fileName, latexMath);

            if (!pdfDone)
                return false;

            return ConvertPdfFileToPngFile(fileName, fileName, resolution);
        }

        public void RenderMathToPdfFiles(IEnumerable<Tuple<string, string>> inputsList)
        {
            foreach (var input in inputsList)
                RenderMathToPdfFile(input.Item1, input.Item2);
        }

        public void RenderMathToPngFiles(IEnumerable<Tuple<string, string>> inputsList, double resolution)
        {
            foreach (var input in inputsList)
                RenderMathToPngFile(input.Item1, input.Item2, resolution);
        }

        public bool ConvertPdfFileToPngFiles(string pdfFileName, double resolution, IReadOnlyList<string> imageFileNames)
        {
            var settings = new MagickReadSettings
            {
                // Setting the density to 300 dpi will create an image with a better quality
                Density = new Density(resolution, resolution)
            };

            var pdfFilePath = Path.Combine(WorkingPath, pdfFileName) + ".pdf";
            using (var images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(pdfFilePath, settings);
                
                var page = 1;
                foreach (var image in images)
                {
                    // Write page to file that contains the page number
                    image.Write(
                        $"{pdfFileName}-{imageFileNames[page - 1]}.png"
                    );

                    //// Writing to a specific format works the same as for a single image
                    //image.Format = MagickFormat.Ptif;
                    //image.Write("Snakeware.Page" + page + ".tif");    
                    page++;
                }
            }

            return true;
        }

        public IEnumerable<Bitmap> ConvertPdfFileToBitmaps(string pdfFileName, double resolution)
        {
            var settings = new MagickReadSettings
            {
                // Setting the density to 300 dpi will create an image with a better quality
                Density = new Density(resolution, resolution)
            };

            var pdfFilePath = Path.Combine(WorkingPath, pdfFileName) + ".pdf";
            using (var images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(pdfFilePath, settings);
                
                var page = 1;
                foreach (var image in images)
                {
                    //image.Resize(new Percentage(50));

                    using (var memStream = new MemoryStream())
                    {
                        image.Write(memStream);
                        yield return (Bitmap)Image.FromStream(memStream);
                    }

                    //// Writing to a specific format works the same as for a single image
                    //image.Format = MagickFormat.Ptif;
                    //image.Write("Snakeware.Page" + page + ".tif");    
                    page++;
                }
            }
        }

        public IEnumerable<byte[]> ConvertPdfFileToByteArrays(string pdfFileName, double resolution)
        {
            var settings = new MagickReadSettings
            {
                // Setting the density to 300 dpi will create an image with a better quality
                Density = new Density(resolution, resolution)
            };

            var pdfFilePath = Path.Combine(WorkingPath, pdfFileName) + ".pdf";
            using (var images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(pdfFilePath, settings);
                
                var page = 1;
                foreach (var image in images)
                {
                    //image.Resize(new Percentage(50));

                    using (var memStream = new MemoryStream())
                    {
                        image.Write(memStream);

                        var byteArray = new byte[memStream.Length];
                        memStream.Read(byteArray, 0, (int)memStream.Length);

                        yield return byteArray;
                    }

                    //// Writing to a specific format works the same as for a single image
                    //image.Format = MagickFormat.Ptif;
                    //image.Write("Snakeware.Page" + page + ".tif");    
                    page++;
                }
            }
        }

        /// <summary>
        /// Render the given GraphViz code into an image
        /// </summary>
        /// <param name="mathText"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public Image RenderMathToImage(string mathText, double resolution = 300)
        {
            var fileName = "texput";

            var pdfDone = 
                RenderMathToPdfFile(fileName, mathText);

            if (!pdfDone)
                return null;

            var bitmaps = 
                ConvertPdfFileToBitmaps(fileName, resolution).ToArray();

            if (bitmaps.Length < 1)
                return null;

            return bitmaps[0];
        }

        public byte[] RenderMathToByteArray(string mathText, double resolution = 300)
        {
            var fileName = "texput";

            var pdfDone = 
                RenderMathToPdfFile(fileName, mathText);

            if (!pdfDone)
                return null;

            var bitmaps = 
                ConvertPdfFileToByteArrays(fileName, resolution).ToArray();

            if (bitmaps.Length < 1)
                return null;

            return bitmaps[0];
        }

        public void RenderMathToImageFile(string filePath, string mathText)
        {
            var image = RenderMathToImage(mathText);

            image.Save(filePath);
        }

        public void RenderMathToImageFile(string filePath, params string[] mathTextLines)
        {
            var image = RenderMathToImage(mathTextLines.Concatenate(@"\\"));

            image.Save(filePath);
        }
    }
}
