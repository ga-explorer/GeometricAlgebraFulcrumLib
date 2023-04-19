namespace GraphicsComposerLib.Rendering.LaTeX.ImageComposers
{
    public interface IGrLaTeXImageComposer
    {
        Image RenderToPngImage(string latexCode);
    
        void RenderToPngFile(string filePath, string latexCode);
    
        void RenderToPngFiles(Func<int, string> filePathFunc, params string[] latexCodeList);

        void RenderToPngFiles(Func<int, string> filePathFunc, IEnumerable<string> latexCodeList);
    }
}