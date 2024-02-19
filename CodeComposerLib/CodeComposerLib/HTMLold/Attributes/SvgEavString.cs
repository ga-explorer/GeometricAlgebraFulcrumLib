using System.Collections.Generic;
using CodeComposerLib.HTMLold.Elements;
using TextComposerLib.Text;

namespace CodeComposerLib.HTMLold.Attributes;

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