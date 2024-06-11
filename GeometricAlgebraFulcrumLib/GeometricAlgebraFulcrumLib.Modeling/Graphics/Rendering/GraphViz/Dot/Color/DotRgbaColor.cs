using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Color;

/// <summary>
/// This class represents an RGBA encoded color value
/// See http://www.graphviz.org/content/attrs#kcolor
/// and http://www.graphviz.org/content/attrs#kcolorList for more details
/// </summary>
public sealed class DotRgbaColor : DotColor
{
    private readonly System.Drawing.Color _color;

    public byte Red => _color.R;

    public byte Green => _color.G;

    public byte Blue => _color.B;

    public byte Alpha => _color.A;

    public System.Drawing.Color ToColor => _color;


    public override string Value
    {
        get
        {
            var s = new StringBuilder(7);

            s.Append('#')
                .Append(Red.ToString("x2"))
                .Append(Green.ToString("x2"))
                .Append(Blue.ToString("x2"))
                .Append(Alpha.ToString("x2"));

            return s.ToString();
        }
    }


    internal DotRgbaColor(System.Drawing.Color color)
    {
        _color = color;
    }

    internal DotRgbaColor(byte red, byte green, byte blue, byte alpha)
    {
        _color = System.Drawing.Color.FromArgb(alpha, red, green, blue);
    }
}