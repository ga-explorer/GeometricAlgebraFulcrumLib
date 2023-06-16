namespace WebComposerLib.LaTeX.ImageComposers
{
    public class WclKaTeXImageComposer :
        IWclLaTeXImageComposer
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