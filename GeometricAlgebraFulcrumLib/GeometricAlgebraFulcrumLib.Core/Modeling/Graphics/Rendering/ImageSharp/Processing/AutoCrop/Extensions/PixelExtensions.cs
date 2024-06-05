using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Extensions;

internal static class PixelExtensions
{
    private static readonly byte _maxBuckets = 10;
    private static readonly double _bucketPrecision = (_maxBuckets + 1) / (double)byte.MaxValue;
    private static readonly double _bucketRatio = byte.MaxValue / _maxBuckets;

    public static byte ToColorBucket(this IPixel color)
    {
        if (color is Rgb24 rgbColor) return rgbColor.ToColorBucket();
        if (color is Rgba32 rgbaColor) return rgbaColor.ToColorBucket();

        throw new NotSupportedException($"Format {color.GetType().Name} is not supported");
    }

    public static byte ToColorBucket(this Rgb24 color)
    {
        return (byte)Math.Min(_maxBuckets, Math.Floor(color.ToGrayscale() * _bucketPrecision));
    }

    public static byte ToColorBucket(this Rgba32 color)
    {
        return (byte)Math.Min(_maxBuckets, Math.Floor(color.ToGrayscale() * _bucketPrecision));
    }

    public static byte ToColorValue(this byte bucket)
    {
        return (byte)Math.Min(byte.MaxValue, bucket * _bucketRatio);
    }

    public static byte ToColorBucket(this byte color)
    {
        return (byte)Math.Min(_maxBuckets, Math.Floor(color * _bucketPrecision));
    }

    public static IPixel<Rgb24> ToColor(this byte bucket)
    {
        var v = bucket.ToColorValue();
        return new Rgb24(v, v, v);
    }

    public static byte ToGrayscale(this Rgb24 color)
    {
        return (byte)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
    }

    public static byte ToGrayscale(this Rgba32 pixel)
    {
        var mix = (byte)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
        if (pixel.A == byte.MaxValue)
            return mix;

        return (byte)Math.Min(byte.MaxValue, mix * (pixel.A * Constants.BytePrecision) + (byte.MaxValue - pixel.A));
    }

    public static byte GetMaxColorBuckets()
    {
        return _maxBuckets;
    }

    public static double GetBucketRatio()
    {
        return _bucketRatio;
    }
}