using System.Globalization;
using OxyPlot;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Colors;

public static class GrColorUtils
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


    public static Color ToImageSharpColor(this System.Drawing.Color color)
    {
        return Color.FromRgba(
            color.R, 
            color.G,
            color.B, 
            color.A
        );
    }

    public static Color ToImageSharpColor(this System.Drawing.Color color, byte alpha)
    {
        return Color.FromRgba(
            color.R, 
            color.G,
            color.B, 
            alpha
        );
    }
    
    public static Color ZeroAlpha(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, 0);
    }
    
    public static Color FullAlpha(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, 255);
    }

    public static Color WithAlpha(this Color color, double alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, (byte) (255 * alpha));
    }

    public static Color SetAlpha(this Color color, byte alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, alpha);
    }
        
    public static Color SetAlpha(this Color color, int alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, (byte) alpha);
    }
        
    public static Color SetAlpha(this Color color, float alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, (byte) (255 * alpha));
        //return Color.FromRgba(c.R, c.G, c.B, (byte) (255 * 1));
    }

    public static Color SetAlpha(this Color color, double alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, (byte) (255 * alpha));
    }
    
    public static Color ScaleRgbBy(this Color color, double scalingFactor)
    {
        var c = color.ToPixel<Rgba32>();

        var r = (byte)double.Clamp(Math.Round(c.R * scalingFactor), 0, 255);
        var g = (byte)double.Clamp(Math.Round(c.G * scalingFactor), 0, 255);
        var b = (byte)double.Clamp(Math.Round(c.B * scalingFactor), 0, 255);
        var a = c.A;

        return Color.FromRgba(r, g, b, a);
    }

    public static Color ToImageSharpColor(this SKColor color)
    {
        return new Rgba32(
            color.Red, 
            color.Green, 
            color.Blue, 
            color.Alpha
        );
    }

    public static SKColor ToSkiaSharpColor(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return new SKColor(
            c.R, c.G, c.B, c.A
        );
    }
        
    public static OxyColor ToOxyColor(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return OxyColor.FromArgb(c.A, c.R, c.G, c.B);
    }
        
    public static OxyColor ToOxyColor(this Color color, byte alpha)
    {
        var c = color.ToPixel<Rgb24>();

        return OxyColor.FromArgb(alpha, c.R, c.G, c.B);
    }

    public static OxyColor ToOxyColor(this System.Drawing.Color color)
    {
        return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
    }
        
    public static OxyColor ToOxyColor(this System.Drawing.Color color, byte alpha)
    {
        return OxyColor.FromArgb(alpha, color.R, color.G, color.B);
    }

    public static System.Drawing.Color ToSystemDrawingColor(this Color color)
    {
        var c = color.ToPixel<Rgba32>();

        return System.Drawing.Color.FromArgb(
            c.A, c.R, c.G, c.B
        );
    }

    public static IEnumerable<System.Drawing.Color> ToSystemDrawingColors(this IEnumerable<Color> colorList)
    {
        return colorList.Select(c => c.ToSystemDrawingColor());
    }
        
    public static IEnumerable<System.Drawing.Color> ToSystemDrawingColors(this Color[] colorList)
    {
        return colorList.Select(c => c.ToSystemDrawingColor()).ToArray();
    }

}