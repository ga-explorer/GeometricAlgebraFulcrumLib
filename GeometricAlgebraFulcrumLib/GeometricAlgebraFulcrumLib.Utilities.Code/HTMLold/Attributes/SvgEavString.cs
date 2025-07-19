using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

public sealed class HtmlEavString<TParentElement>
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    protected override string ValueComputedText => string.Empty;


    internal HtmlEavString(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
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

    //public TParentElement SetTo(params string[] values)
    //{
    //    ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

    //    return ParentElement;
    //}

    public TParentElement SetTo(IEnumerable<string> values)
    {
        ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

        return ParentElement;
    }
}