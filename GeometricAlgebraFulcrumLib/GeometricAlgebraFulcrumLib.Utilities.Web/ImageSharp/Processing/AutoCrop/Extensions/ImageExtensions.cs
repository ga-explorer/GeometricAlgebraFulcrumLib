using System.Reflection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Extensions;

internal static class ImageExtensions
{
    public static void CopyRect<TPixel>(this Image<TPixel> target, Image<TPixel> source, Rectangle bounds, Point offset) where TPixel : unmanaged, IPixel<TPixel>
    {
        source.ProcessPixelRows(target, (sourceAccessor, targetAccessor) =>
        {
            for (var y = bounds.Top; y < bounds.Bottom; y++)
            {
                var srow = sourceAccessor.GetRowSpan(y);
                var tRow = targetAccessor.GetRowSpan(y + offset.Y);

                for (var x = bounds.Left; x < bounds.Right; x++)
                {
                    tRow[x + offset.X] = srow[x];
                }
            }
        });
    }

    public static void SwapPixelBuffersFrom<TPixel>(this Image<TPixel> image, Image<TPixel> pixelSource) where TPixel : unmanaged, IPixel<TPixel>
    {
        var imageType = image.GetType();
        var copyMethod = imageType.GetMethod("SwapOrCopyPixelsBuffersFrom", BindingFlags.NonPublic | BindingFlags.Instance);

        copyMethod.Invoke(image, new[] { pixelSource });
    }
}