using System.Text;
using CodeComposerLib.HTMLold.Elements;
using CodeComposerLib.HTMLold.Values;
using TextComposerLib;

namespace CodeComposerLib.HTMLold.Attributes;

/// <summary>
/// This class holds information about the value of an attribute for a parent element
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public abstract class HtmlElementAttributeValue<TParentElement> 
    : IHtmlAttributeValue where TParentElement : HtmlElement
{
    public TParentElement ParentElement { get; }

    protected abstract string ValueComputedText { get; }

    private string _valueStoredText = string.Empty;
    protected string ValueStoredText
    {
        get { return _valueStoredText; }
        set
        {
            _valueStoredText = value ?? string.Empty;
            IsValueComputed = false;
        }
    }

    public bool IsValueComputed { get; protected set; }

    public bool IsValueStored => !IsValueComputed;

    public bool IsValueEmpty => string.IsNullOrEmpty(ValueText);

    public string ValueText
    {
        get
        {
            if (IsValueStored)
                return ValueStoredText;

            ValueStoredText = ValueComputedText;
            IsValueComputed = false;

            return ValueStoredText;
        }
    }

    public HtmlAttributeInfo AttributeInfo { get; }

    public int AttributeId => AttributeInfo.Id;

    public string AttributeName => AttributeInfo.Name;


    protected HtmlElementAttributeValue(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
    {
        ParentElement = parentElement;
        AttributeInfo = attributeInfo;
    }


    public abstract IHtmlAttributeValue CreateCopy();

    public abstract IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue);

    public TParentElement SetToText(string value)
    {
        ValueStoredText = value;

        return ParentElement;
    }

    public TParentElement SetToText(IHtmlValue value)
    {
        ValueStoredText = value.ValueText;

        return ParentElement;
    }

    public TParentElement SetToInherit()
    {
        ValueStoredText = "inherit";

        return ParentElement;
    }

    public TParentElement SetToEmptyDefault()
    {
        ValueStoredText = string.Empty;

        return ParentElement;
    }

    public override string ToString()
    {
        return new StringBuilder().Append(AttributeInfo.Name)
            .Append("=")
            .Append(ValueText.ToHtmlSafeLiteral())
            .ToString();
    }
}