using CodeComposerLib.LaTeX;

namespace CodeComposerLib.GraphViz.Dot.Image
{
    public sealed class DotGraphLaTeXImage : IDotGraphImage
    {
        public static LaTeXRenderer LaTeXRenderer { get; }
            = new LaTeXRenderer();

        public string ImageId { get; }

        public string ImageFileName
            => ImageId + ".png";

        public bool IsGenerated 
            => true;

        public double Resolution { get; set; } = 96;

        public string LaTeXMath { get; set; }


        internal DotGraphLaTeXImage(string imageId)
        {
            ImageId = imageId;
        }


        public System.Drawing.Image GetImage()
        {
            return LaTeXRenderer.RenderMathToImage(LaTeXMath, 150);
        }


        public override string ToString()
        {
            return LaTeXMath;
        }
    }
}