using System.Globalization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images
{
    public static class GrVisualImageUtils
    {
        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>A hexadecimal string representation of the value.</returns>
        public static string RgbToHexString(this Color color)
        {
            var c = color.ToPixel<Rgb24>();

            var hexOrder = (uint)(c.B << 0 | c.G << 8 | c.R << 16);

            return hexOrder.ToString("X6", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>A hexadecimal string representation of the value.</returns>
        public static string RgbaToHexString(this Color color)
        {
            var c = color.ToPixel<Rgba32>();

            var hexOrder = (uint)(c.A << 0 | c.B << 8 | c.G << 16 | c.R << 24);

            return hexOrder.ToString("X8", CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// imageToProcess.Mutate(i => i.Clear(Color.Transparent);
        /// https://github.com/SixLabors/ImageSharp.Drawing/issues/26
        /// </summary>
        /// <param name="source"></param>
        /// <param name="color"></param>
        public static void Clear(this IImageProcessingContext source, Color color)
        {
            var drawingOptions = new DrawingOptions
            {
                GraphicsOptions = new GraphicsOptions()
                {
                    //AlphaCompositionMode = PixelAlphaCompositionMode.Clear
                    AlphaCompositionMode = PixelAlphaCompositionMode.Src,
                    ColorBlendingMode = PixelColorBlendingMode.Normal
                }
            };

            var size = source.GetCurrentSize();

            source.Fill(
                drawingOptions,
                color,
                new RectangleF(0, 0, size.Width, size.Height)
            );
        }


        public static Image Create(int width, int height, Func<int, int, Color> imageFunc)
        {
            var bitmap = new Image<Rgba32>(width, height);

            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    bitmap[i, j] = imageFunc(i, j);
                }

            return bitmap;
        }

        public static Image CreateColorGrid(int squareSize, Color[,] colorArray)
        {
            var rowCount = colorArray.GetLength(0);
            var columnCount = colorArray.GetLength(1);
            var width = squareSize * columnCount;
            var height = squareSize * rowCount;
            var bitmap = new Image<Rgba32>(width, height);

            for (var i = 0; i < rowCount; i++)
            {
                var i1 = i * squareSize;

                for (var j = 0; j < columnCount; j++)
                {
                    var j1 = j * squareSize;

                    var color = colorArray[i, j];

                    for (var x = 0; x < squareSize; x++)
                        for (var y = 0; y < squareSize; y++)
                            bitmap[x + i1, y + j1] = color;
                }
            }

            return bitmap;
        }

        public static GrVisualStoredImageTexture ToStoredImageTexture(this Image image)
        {
            return new GrVisualStoredImageTexture(image);
        }

        public static GrVisualStoredImageTexture GetImageAsTexture(this IGrVisualImageComposer composer)
        {
            return new GrVisualStoredImageTexture(composer.GetImage());
        }
    }
}
