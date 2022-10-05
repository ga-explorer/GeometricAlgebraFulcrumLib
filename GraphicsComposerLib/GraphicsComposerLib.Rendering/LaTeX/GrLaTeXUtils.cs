using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.LaTeX
{
    public static class GrLaTeXUtils
    {
        public static void RenderToPngFile(this IGrLaTeXImageComposer renderer, string filePath, params string[] mathTextLines)
        {
            renderer.RenderToPngFile(
                filePath, 
                mathTextLines.Concatenate(@"\\")
            );
        }

        public static void RenderToPngFile(this IGrLaTeXImageComposer renderer, string filePath, IEnumerable<string> mathTextLines)
        {
            renderer.RenderToPngFile(
                filePath, 
                mathTextLines.Concatenate(@"\\")
            );
        }

        public static void RenderToPngFiles(this IGrLaTeXImageComposer renderer, Func<int, string> filePathFunc, params string[] mathTextLines)
        {
            for (var i = 0; i < mathTextLines.Length; i++)
                renderer.RenderToPngFile(
                    filePathFunc(i), 
                    mathTextLines[i]
                );
        }

        public static void RenderToPngFiles(this IGrLaTeXImageComposer renderer, Func<int, string> filePathFunc, IEnumerable<string> mathTextLines)
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
}
