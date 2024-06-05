using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.ImageComposers;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX;

public static class WclLaTeXUtils
{
    public static void RenderToPngFile(this IWclLaTeXImageComposer renderer, string filePath, params string[] mathTextLines)
    {
        renderer.RenderToPngFile(
            filePath, 
            mathTextLines.Concatenate(@"\\")
        );
    }

    public static void RenderToPngFile(this IWclLaTeXImageComposer renderer, string filePath, IEnumerable<string> mathTextLines)
    {
        renderer.RenderToPngFile(
            filePath, 
            mathTextLines.Concatenate(@"\\")
        );
    }

    public static void RenderToPngFiles(this IWclLaTeXImageComposer renderer, Func<int, string> filePathFunc, params string[] mathTextLines)
    {
        for (var i = 0; i < mathTextLines.Length; i++)
            renderer.RenderToPngFile(
                filePathFunc(i), 
                mathTextLines[i]
            );
    }

    public static void RenderToPngFiles(this IWclLaTeXImageComposer renderer, Func<int, string> filePathFunc, IEnumerable<string> mathTextLines)
    {
        var i = 0;
        foreach (var mathTextLine in mathTextLines)
        {
            renderer.RenderToPngFile(
                filePathFunc(i),
                mathTextLine
            );

            i++;
        }
    }
}