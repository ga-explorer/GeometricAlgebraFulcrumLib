using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

public sealed class HtmlEavLength<TParentElement>
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    private double _length;
    public double Length
    {
        get { return _length; }
        set
        {
            _length = value;
            IsValueComputed = true;
        }
    }

    private HtmlValueLengthUnit _unit = HtmlValueLengthUnit.None;
    public HtmlValueLengthUnit Unit
    {
        get { return _unit; }
        set
        {
            _unit = value ?? HtmlValueLengthUnit.None;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText
        => _length.ToHtmlLengthText(_unit);


    internal HtmlEavLength(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
    {
    }


    public override IHtmlAttributeValue CreateCopy()
    {
        throw new System.NotImplementedException();
    }

    public override IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue)
    {
        throw new System.NotImplementedException();
    }

    public TParentElement SetTo(double length)
    {
        Length = length;
        Unit = HtmlValueLengthUnit.None;

        return ParentElement;
    }

    public TParentElement SetTo(double length, HtmlValueLengthUnit unit)
    {
        Length = length;
        Unit = unit;

        return ParentElement;
    }
}