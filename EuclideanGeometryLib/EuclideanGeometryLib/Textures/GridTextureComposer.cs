using System.Drawing;

namespace EuclideanGeometryLib.Textures
{
    public sealed class GridTextureComposer
    {
        public static string Pattern1 { get; private set; }

        public static string Pattern2 { get; private set; }

        public static string Pattern3 { get; private set; }

        static GridTextureComposer()
        {
            Pattern1 = "||-------|-------|-------||-------|-------|-------||";
            Pattern2 = "||-----------------------||-----------------------||";
            Pattern3 = "||------------------------------------------------||";
        }


        public string XPattern { get; set; } = Pattern2;

        public string YPattern { get; set; } = Pattern2;

        public Color BackgroundColor { get; set; } = Color.White;

        public Color LineColor { get; set; } = Color.CornflowerBlue;

        public int LineWidth { get; set; } = 1;

        public int ImageWidth => LineWidth * XPattern.Length;

        public int ImageHeight => LineWidth * YPattern.Length;


        public Image ComposeImage()
        {
            var image = new Bitmap(ImageWidth, ImageHeight);

            for (var x = 0; x < ImageWidth; x++)
            {
                var i = x / LineWidth;
                var xFlag = XPattern[i] == '|';

                for (var y = 0; y < ImageHeight; y++)
                {
                    var j = y / LineWidth;

                    var c = xFlag || YPattern[j] == '|'
                        ? LineColor
                        : BackgroundColor;

                    image.SetPixel(x, y, c);
                }
            }

            return image;
        }
    }
}
