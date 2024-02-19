using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Attributes;

namespace WebComposerLib.Svg.Styles.Properties;

public sealed class SvgSpvNumber : SvgStylePropertyValue
{
    private double _number;
    public double Number
    {
        get => _number;
        set
        {
            _number = value;
            IsValueComputed = true;
        }
    }

    private bool _isPercent;
    public bool IsPercent
    {
        get => _isPercent;
        set
        {
            _isPercent = value;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => _number.ToSvgNumberText(IsPercent);


    internal SvgSpvNumber(SvgStyle parentStyle, SvgAttributeInfo attributeInfo)
        : base(parentStyle, attributeInfo)
    {
    }


    public override SvgStylePropertyValue CreateCopy()
    {
        var result = new SvgSpvNumber(ParentStyle, AttributeInfo);

        if (IsValueStored)
        {
            result._number = _number;
            result._isPercent = _isPercent;
            result.ValueStoredText = ValueStoredText;

            return result;
        }

        result.Number = Number;
        result.IsPercent = IsPercent;

        return result;
    }

    public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
    {
        var source = sourcePropertyValue as SvgSpvNumber;

        if (ReferenceEquals(source, null) || source.IsValueStored)
        {
            ValueStoredText = source?.ValueStoredText;

            return this;
        }

        Number = source.Number;
        IsPercent = source.IsPercent;

        return this;
    }

    public SvgStyle SetToNumber(double number)
    {
        Number = number;
        IsPercent = false;

        return ParentStyle;
    }

    public SvgStyle SetToPercent(double number)
    {
        Number = number;
        IsPercent = true;

        return ParentStyle;
    }
}