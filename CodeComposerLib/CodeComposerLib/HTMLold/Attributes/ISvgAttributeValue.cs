namespace CodeComposerLib.HTMLold.Attributes;

public interface IHtmlAttributeValue
{
    string ValueText { get; }

    HtmlAttributeInfo AttributeInfo { get; }

    int AttributeId { get; }

    string AttributeName { get; }


}