using System.Diagnostics;
using System.Text;
using DataStructuresLib.Files;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Templates;
using GraphicsComposerLib.Rendering.Pdf;
using TextComposerLib;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.LaTeX.ImageComposers
{
    public class GrLaTeXImageComposer :
        IGrLaTeXImageComposer
    {
        ///// <summary>
        ///// ImageConverter object used to convert byte arrays containing JPEG or PNG file images into 
        ///// Bitmap objects. This is static and only gets instantiated once.
        ///// </summary>
        //private static ImageConverter ImageConverter { get; } 
        //    = new ImageConverter();

        private readonly List<string> _errorsList = new List<string>();
        private string _laTeXBinFolder = @"D:\texlive\2021\bin\win32";


        public double Resolution { get; set; } 
            = 300d;
        
        /// <summary>
        /// Get or set the executable bin folder of pdflatex.exe
        /// </summary>
        public string LaTeXBinFolder
        {
            get => _laTeXBinFolder;
            set
            {
                if (Directory.Exists(value))
                    _laTeXBinFolder = value;

                else
                    throw new DirectoryNotFoundException(value);
            }
        }

        public string PdfLaTeXFilePath
            => LaTeXBinFolder.GetFilePath("pdflatex", "exe");

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
        private string GetPdfLaTeXArguments(string pdfFileName)
        {
            return $"-jobname={pdfFileName} {pdfFileName}";
            //return $"-jobname={pdfFileName}";
        }

        /// <summary>
        /// Create a ProcessStartInfo object for pdflatex.exe
        /// </summary>
        /// <returns></returns>
        private ProcessStartInfo GetPdfLaTeXProcessStartInfo(string workingPath, string pdfFileName)
        {
            return
                new ProcessStartInfo
                {
                    WorkingDirectory = workingPath,
                    FileName = PdfLaTeXFilePath.ValueToQuotedLiteral(),
                    RedirectStandardInput = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    Arguments = GetPdfLaTeXArguments(pdfFileName),
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

        public bool RenderToPdfFile(string filePath, string latexCode)
        {
            _errorsList.Clear();

            var (workingFolderPath, fileName, _) = filePath.GetFilePathParts();

            var texFilePath = workingFolderPath.GetFilePath(fileName, "tex");
            var auxFilePath = workingFolderPath.GetFilePath(fileName, "aux");
            var logFilePath = workingFolderPath.GetFilePath(fileName, "log");

            File.WriteAllText(texFilePath, latexCode);

            var processStartInfo = GetPdfLaTeXProcessStartInfo(workingFolderPath, fileName);
            using var process = Process.Start(processStartInfo);

            if (ReferenceEquals(process, null)) 
                return false;

            process.ErrorDataReceived += ErrorDataHandler;
            process.BeginErrorReadLine();

            while (!process.HasExited) ;

            texFilePath.TryDeleteFile();
            auxFilePath.TryDeleteFile();
            logFilePath.TryDeleteFile();

            Debug.Assert(!HasRenderingErrors);

            return !HasRenderingErrors;
        }

        public bool RenderToPdfFile(string filePath, IEnumerable<string> latexCodeList)
        {
            _errorsList.Clear();

            var contentsString = 
                latexCodeList.Concatenate(
                    " \\newpage" + Environment.NewLine
                );

            var latexSourceCode = 
                LaTeXTemplates.MathPreview1.Replace(
                    "#contents#", 
                    contentsString
                );

            var (workingFolderPath, fileName, _) = filePath.GetFilePathParts();

            var texFilePath = workingFolderPath.GetFilePath(fileName, "tex");
            var auxFilePath = workingFolderPath.GetFilePath(fileName, "aux");
            var logFilePath = workingFolderPath.GetFilePath(fileName, "log");

            File.WriteAllText(texFilePath, latexSourceCode);

            var processStartInfo = GetPdfLaTeXProcessStartInfo(workingFolderPath, fileName);
            using var process = Process.Start(processStartInfo);

            if (ReferenceEquals(process, null)) 
                return false;

            process.ErrorDataReceived += ErrorDataHandler;
            process.BeginErrorReadLine();

            while (!process.HasExited) ;

            texFilePath.TryDeleteFile();
            auxFilePath.TryDeleteFile();
            logFilePath.TryDeleteFile();
            
            Debug.Assert(!HasRenderingErrors);

            return !HasRenderingErrors;
        }


        public void RenderToPngFile(string pdfFilePath, string latexCode)
        {
            var pdfDone = RenderToPdfFile(pdfFilePath, latexCode);

            var (workingFolderPath, fileName, _) = pdfFilePath.GetFilePathParts();
            var pngFilePath = workingFolderPath.GetFilePath(fileName, "png");

            if (pdfDone)
                pdfFilePath.PdfFileToPngFile(pngFilePath, Resolution);
        }

        public void RenderToPdfFiles(IEnumerable<Tuple<string, string>> inputsList)
        {
            foreach (var input in inputsList)
                RenderToPdfFile(input.Item1, input.Item2);
        }

        public void RenderToPngFiles(IEnumerable<Tuple<string, string>> inputsList)
        {
            foreach (var input in inputsList)
                RenderToPngFile(input.Item1, input.Item2);
        }
        
        public void RenderToPngFiles(Func<int, string> filePathFunc, params string[] latexCodeList)
        {
            var pdfFilePath = "pdf".GetTempFilePath();

            RenderToPdfFile(pdfFilePath, latexCodeList);

            pdfFilePath.PdfFileToPngFiles(Resolution, filePathFunc);
        }

        public void RenderToPngFiles(Func<int, string> filePathFunc, IEnumerable<string> latexCodeList)
        {
            var pdfFilePath = "pdf".GetTempFilePath();

            RenderToPdfFile(pdfFilePath, latexCodeList);

            pdfFilePath.PdfFileToPngFiles(Resolution, filePathFunc);
        }
        
        public Image RenderToPngImage(string latexCode)
        {
            var pdfFilePath = "pdf".GetTempFilePath();

            var pdfDone = 
                RenderToPdfFile(pdfFilePath, latexCode);

            if (!pdfDone)
                return null;

            var imageArray = 
                pdfFilePath.PdfFileToImages(Resolution).ToArray();

            return imageArray.FirstOrDefault() ?? null;
        }

        public byte[] RenderToPngByteArray(string latexCode)
        {
            var pdfFilePath = "pdf".GetTempFilePath();

            var pdfDone = 
                RenderToPdfFile(pdfFilePath, latexCode);

            if (!pdfDone)
                return null;

            var byteArrays = 
                pdfFilePath.PdfFileToPngByteArrays(Resolution).ToArray();

            return byteArrays.FirstOrDefault() ?? null;
        }
        
    }
}
