using CodeComposerLib.HTMLold.Attributes;
using CodeComposerLib.HTMLold.Content;

namespace CodeComposerLib.HTMLold.Elements;

/// <summary>
/// http://docs.w3cub.com/svg/element/
/// </summary>
public interface IHtmlElement : IHtmlContent
{
    HtmlContentsList Contents { get; }

    string ElementName { get; }

    string Id { get; }

    string ContentsText { get; }

    string AttributesText { get; }

    string BeginEndTagText { get; }

    string BeginTagText { get; }

    string EndTagText { get; }

    string TagText { get; }

    IHtmlElement ClearAttributes();

    IHtmlElement ClearAttribute(HtmlAttributeInfo attributeInfo);

    IHtmlElement ClearAttributes(params HtmlAttributeInfo[] attributeInfoList);

    IHtmlElement ClearDefaultAttributes(bool clearInChildren);
}