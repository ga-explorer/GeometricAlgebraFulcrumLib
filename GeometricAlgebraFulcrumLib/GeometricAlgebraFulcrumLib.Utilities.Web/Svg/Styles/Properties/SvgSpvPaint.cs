using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;

/// <inheritdoc />
/// <summary>
/// https://www.w3.org/TR/SVG/painting.html#SpecifyingPaint
/// </summary>
public sealed class SvgSpvPaint : SvgStylePropertyValue
{
    private SvgConstants.ColorSpecs _colorSpecs = SvgConstants.ColorSpecs.ColorValue;
    public SvgConstants.ColorSpecs ColorSpecs
    {
        get => _colorSpecs;
        set
        {
            _colorSpecs = value;
            IsValueComputed = true;
        }
    }

    private string _paintServer;
    public string PaintServer
    {
        get => _paintServer;
        set
        {
            _paintServer = value ?? string.Empty;
            IsValueComputed = true;
        }
    }

    private Color _colorValue;
    public Color ColorValue
    {
        get => _colorValue;
        set
        {
            _colorValue = value;
            _colorSpecs = SvgConstants.ColorSpecs.ColorValue;
            IsValueComputed = true;
        }
    }

    private string _iccColorValue;
    public string IccColorValue
    {
        get => _iccColorValue;
        set
        {
            _iccColorValue = value ?? string.Empty;
            _colorSpecs = SvgConstants.ColorSpecs.ColorValue;
            IsValueComputed = true;
        }
    }


    protected override string ValueComputedText
    {
        get
        {
            var s = new StringBuilder();

            if (!string.IsNullOrEmpty(PaintServer))
                s.Append("url(#")
                    .Append(PaintServer)
                    .Append(") ");

            if (ColorSpecs == SvgConstants.ColorSpecs.None)
                s.Append("none");

            else if (ColorSpecs == SvgConstants.ColorSpecs.CurrentColor)
                s.Append("currentColor");

            else
                s.Append(ColorValue.ToSvgColorHexText())
                    .Append(IccColorValue);

            return s.ToString();
        }
    }


    internal SvgSpvPaint(SvgStyle parentElement, SvgAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
    {
    }


    public override SvgStylePropertyValue CreateCopy()
    {
        var result = new SvgSpvPaint(ParentStyle, AttributeInfo);

        if (IsValueStored)
        {
            result._colorSpecs = _colorSpecs;
            result._paintServer = _paintServer;
            result._colorValue = _colorValue;
            result._iccColorValue = _iccColorValue;
            result.ValueStoredText = ValueStoredText;

            return result;
        }

        result.ColorSpecs = ColorSpecs;
        result.PaintServer = PaintServer;
        result.ColorValue = ColorValue;
        result.IccColorValue = IccColorValue;

        return result;
    }

    public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
    {
        var source = sourcePropertyValue as SvgSpvPaint;

        if (ReferenceEquals(source, null) || source.IsValueStored)
        {
            ValueStoredText = source?.ValueStoredText;

            return this;
        }

        ColorSpecs = source.ColorSpecs;
        PaintServer = source.PaintServer;
        ColorValue = source.ColorValue;
        IccColorValue = source.IccColorValue;

        return this;
    }

    public SvgStyle SetToNone()
    {
        PaintServer = string.Empty;
        ColorSpecs = SvgConstants.ColorSpecs.None;

        return ParentStyle;
    }

    public SvgStyle SetToNone(string paintServer)
    {
        PaintServer = paintServer;
        ColorSpecs = SvgConstants.ColorSpecs.None;

        return ParentStyle;
    }

    public SvgStyle SetToCurrentColor()
    {
        PaintServer = string.Empty;
        ColorSpecs = SvgConstants.ColorSpecs.CurrentColor;

        return ParentStyle;
    }

    public SvgStyle SetToCurrentColor(string paintServer)
    {
        PaintServer = paintServer;
        ColorSpecs = SvgConstants.ColorSpecs.CurrentColor;

        return ParentStyle;
    }

    public SvgStyle SetToRgb(Color value)
    {
        PaintServer = string.Empty;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = value;

        return ParentStyle;
    }

    public SvgStyle SetToRgb(string paintServer, Color value)
    {
        PaintServer = paintServer;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = value;

        return ParentStyle;
    }

    public SvgStyle SetToRgbPercent(double red, double green, double blue)
    {
        PaintServer = string.Empty;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = Color.FromRgb(
            (byte) Math.Round(red * 255),
            (byte) Math.Round(green * 255),
            (byte) Math.Round(blue * 255)
        );

        return ParentStyle;
    }

    public SvgStyle SetToRgbPercent(string paintServer, double red, double green, double blue)
    {
        PaintServer = paintServer;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = Color.FromRgb(
            (byte) Math.Round(red * 255),
            (byte) Math.Round(green * 255),
            (byte) Math.Round(blue * 255)
        );

        return ParentStyle;
    }

    public SvgStyle SetToRgb(int red, int green, int blue)
    {
        PaintServer = string.Empty;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = Color.FromRgb(
            (byte) red, 
            (byte) green, 
            (byte) blue
        );

        return ParentStyle;
    }

    public SvgStyle SetToRgb(string paintServer, int red, int green, int blue)
    {
        PaintServer = paintServer;
        ColorSpecs = SvgConstants.ColorSpecs.ColorValue;
        ColorValue = Color.FromRgb(
            (byte) red, 
            (byte) green, 
            (byte) blue
        );

        return ParentStyle;
    }
}