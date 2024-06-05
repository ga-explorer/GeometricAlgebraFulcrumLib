using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

public sealed class HtmlEavAttribute<TParentElement>
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    private HtmlAttributeInfo _attribute;
    public HtmlAttributeInfo Attribute
    {
        get { return _attribute; }
        set
        {
            _attribute = value;
            IsValueComputed = true;
        }
    }

    protected override string ValueComputedText => _attribute.Name;


    internal HtmlEavAttribute(TParentElement parentElement)
        : base(parentElement, HtmlAttributeUtils.AttributeName)
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

    public TParentElement SetTo(HtmlAttributeInfo value)
    {
        Attribute = value;

        return ParentElement;
    }
}