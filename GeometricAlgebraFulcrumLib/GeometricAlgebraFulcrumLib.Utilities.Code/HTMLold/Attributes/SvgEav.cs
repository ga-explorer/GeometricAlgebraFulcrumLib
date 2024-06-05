using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

public sealed class HtmlEav<TValueType, TParentElement>
    : HtmlElementAttributeValue<TParentElement>
    where TParentElement : HtmlElement
    where TValueType : IHtmlValue
{
    //TODO: Implement default value computation based on parent element type
    private TValueType _value;
    public TValueType Value
    {
        get { return _value; }
        set
        {
            _value = value;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText => Value?.ValueText ?? string.Empty;


    internal HtmlEav(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
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

    public TParentElement SetTo(TValueType value)
    {
        Value = value;

        return ParentElement;
    }
}