namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Extensions;

internal static class ColorExtensions
{
    public static byte ToColorBucket(this Color color)
    {
        return color.ToPixel<Rgba32>().ToColorBucket();
    }
}