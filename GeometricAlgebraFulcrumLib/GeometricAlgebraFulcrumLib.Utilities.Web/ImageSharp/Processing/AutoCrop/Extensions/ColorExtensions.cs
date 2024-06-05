using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Extensions;

internal static class ColorExtensions
{
    public static byte ToColorBucket(this Color color)
    {
        return color.ToPixel<Rgba32>().ToColorBucket();
    }
}