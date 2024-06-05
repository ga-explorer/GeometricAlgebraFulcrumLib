using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.ImageComposers;

public interface IWclLaTeXImageComposer
{
    Image RenderToPngImage(string latexCode);
    
    void RenderToPngFile(string filePath, string latexCode);
    
    void RenderToPngFiles(Func<int, string> filePathFunc, params string[] latexCodeList);

    void RenderToPngFiles(Func<int, string> filePathFunc, IEnumerable<string> latexCodeList);
}