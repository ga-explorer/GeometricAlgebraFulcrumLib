using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Styles.Properties;

public sealed class SvgSpvLength : SvgStylePropertyValue
{
    private double _length;
    public double Length
    {
        get => _length;
        set
        {
            _length = value;
            IsValueComputed = true;
        }
    }

    private SvgLengthUnit _unit = SvgLengthUnit.None;
    public SvgLengthUnit Unit
    {
        get => _unit;
        set
        {
            _unit = value ?? SvgLengthUnit.None;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => _length.ToSvgLengthText(_unit);


    internal SvgSpvLength(SvgStyle parentElement, SvgAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
    {
    }


    public override SvgStylePropertyValue CreateCopy()
    {
        var result = new SvgSpvLength(ParentStyle, AttributeInfo);

        if (IsValueStored)
        {
            result._length = _length;
            result._unit = _unit;
            result.ValueStoredText = ValueStoredText;

            return result;
        }

        result.Length = Length;
        result.Unit = Unit;

        return result;
    }

    public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
    {
        var source = sourcePropertyValue as SvgSpvLength;

        if (ReferenceEquals(source, null) || source.IsValueStored)
        {
            ValueStoredText = source?.ValueStoredText;

            return this;
        }

        Length = source.Length;
        Unit = source.Unit;

        return this;
    }

    public SvgStyle SetTo(double length)
    {
        Length = length;
        Unit = SvgLengthUnit.None;

        return ParentStyle;
    }

    public SvgStyle SetTo(double length, SvgLengthUnit unit)
    {
        Length = length;
        Unit = unit;

        return ParentStyle;
    }
}