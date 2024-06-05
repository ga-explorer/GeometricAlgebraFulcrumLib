using OxyPlot;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Colors;

public static class WclColorUtils
{
        
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
    }

    public static Color SetAlpha(this Color color, double alpha)
    {
        var c = color.ToPixel<Rgba32>();

        return Color.FromRgba(c.R, c.G, c.B, (byte) (255 * alpha));
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