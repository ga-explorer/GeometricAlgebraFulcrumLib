namespace GraphicsComposerLib.Rendering.LaTeX.ImageComposers
{
    public class GrKaTeXImageComposer :
        IGrLaTeXImageComposer
    {
        public Image RenderToPngImage(string latexCode)
        {
            throw new NotImplementedException();
        }

        public void RenderToPngFile(string filePath, string latexCode)
        {
            throw new NotImplementedException();
        }

        public void RenderToPngFiles(Func<int, string> filePathFunc, params string[] latexCodeList)
        {
            throw new NotImplementedException();
        }

        public void RenderToPngFiles(Func<int, string> filePathFunc, IEnumerable<string> latexCodeList)
        {
            throw new NotImplementedException();
        }
    }
}