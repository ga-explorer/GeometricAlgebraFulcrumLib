namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color;

/// <summary>
/// This class represents a dot color value
/// See http://www.graphviz.org/content/attrs#kcolor
/// and http://www.graphviz.org/content/attrs#kcolorList for more details
/// </summary>
public abstract class DotColor : DotValue
{
    /// <summary>
    /// The transparent color value
    /// </summary>
    public static readonly DotNamedColor Transparent = new DotNamedColor("transparent");

    /// <summary>
    /// Create an indexed color value
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static DotIndexedColor Indexed(int c)
    {
        return new DotIndexedColor(c);
    }

    /// <summary>
    /// Create an RGB color value
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <returns></returns>
    public static DotRgbColor Rgb(byte red, byte green, byte blue)
    {
        return new DotRgbColor(red, green, blue);
    }

    /// <summary>
    /// Create an RGB color value
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static DotRgbColor Rgb(System.Drawing.Color c)
    {
        return new DotRgbColor(c);
    }

    /// <summary>
    /// Create an RGBA color value
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static DotRgbaColor Rgba(byte red, byte green, byte blue, byte alpha)
    {
        return new DotRgbaColor(red, green, blue, alpha);
    }

    /// <summary>
    /// Create an RGBA color value
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static DotRgbaColor Rgba(System.Drawing.Color c)
    {
        return new DotRgbaColor(c);
    }

    /// <summary>
    /// Create an HSV color value
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static DotHsvColor Hsv(float h, float s, float v)
    {
        return new DotHsvColor(h, s, v);
    }

    /// <summary>
    /// Create an HSV color value
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static DotHsvColor Hsv(System.Drawing.Color c)
    {
        return new DotHsvColor(c);
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(int c1, int c2)
    {
        return new DotColorList(
            new DotColor[] { new DotIndexedColor(c1), new DotIndexedColor(c2) }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="w1"></param>
    /// <param name="w2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(int c1, int c2, float w1, float w2)
    {
        return new DotColorList(
            new DotColor[] { new DotIndexedColor(c1), new DotIndexedColor(c2) },
            new [] { w1, w2 }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(DotColor c1, DotColor c2)
    {
        return new DotColorList(
            new [] { c1, c2 }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="w1"></param>
    /// <param name="w2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(DotColor c1, DotColor c2, float w1, float w2)
    {
        return new DotColorList(
            new[] { c1, c2 },
            new[] { w1, w2 }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(System.Drawing.Color c1, System.Drawing.Color c2)
    {
        return new DotColorList(
            new DotColor[] { new DotRgbaColor(c1), new DotRgbaColor(c2) }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="w1"></param>
    /// <param name="w2"></param>
    /// <returns></returns>
    public static DotColorList ColorList(System.Drawing.Color c1, System.Drawing.Color c2, float w1, float w2)
    {
        return new DotColorList(
            new DotColor[] { new DotRgbaColor(c1), new DotRgbaColor(c2) },
            new[] { w1, w2 }
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<int> colors)
    {
        return new DotColorList(
            colors.Select(c => new DotIndexedColor(c))
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <param name="weights"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<int> colors, IEnumerable<float> weights)
    {
        return new DotColorList(
            colors.Select(c => new DotIndexedColor(c)),
            weights
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<System.Drawing.Color> colors)
    {
        return new DotColorList(
            colors.Select(c => new DotRgbaColor(c))
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <param name="weights"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<System.Drawing.Color> colors, IEnumerable<float> weights)
    {
        return new DotColorList(
            colors.Select(c => new DotRgbaColor(c)),
            weights
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<DotColor> colors)
    {
        return new DotColorList(colors);
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <param name="weights"></param>
    /// <returns></returns>
    public static DotColorList ColorList(IEnumerable<DotColor> colors, IEnumerable<float> weights)
    {
        return new DotColorList(colors, weights);
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(params int[] colors)
    {
        return new DotColorList(
            colors.Select(c => new DotIndexedColor(c))
        );
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(params DotColor[] colors)
    {
        return new DotColorList(colors);
    }

    /// <summary>
    /// Create a color list value
    /// </summary>
    /// <param name="colors"></param>
    /// <returns></returns>
    public static DotColorList ColorList(params System.Drawing.Color[] colors)
    {
        return new DotColorList(
            colors.Select(c => new DotRgbaColor(c))
        );
    }
}