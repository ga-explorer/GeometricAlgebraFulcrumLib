using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;

/// <summary>
/// This class holds information about the value of an attribute for a parent element
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public abstract class SvgElementAttributeValue<TParentElement> : 
    ISvgAttributeValue 
    where TParentElement : SvgElement
{
    public TParentElement ParentElement { get; }

    protected abstract string ValueComputedText { get; }

    private string _valueStoredText = string.Empty;
    protected string ValueStoredText
    {
        get => _valueStoredText;
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

    public SvgAttributeInfo AttributeInfo { get; }

    public int AttributeId => AttributeInfo.Id;

    public string AttributeName => AttributeInfo.Name;


    protected SvgElementAttributeValue(TParentElement parentElement, SvgAttributeInfo attributeInfo)
    {
        ParentElement = parentElement;
        AttributeInfo = attributeInfo;
    }


    public abstract ISvgAttributeValue CreateCopy();

    public abstract ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue);

    public TParentElement SetToText(string value)
    {
        ValueStoredText = value;

        return ParentElement;
    }

    public TParentElement SetToText(ISvgValue value)
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
        return new StringBuilder()
            .Append(AttributeInfo.Name)
            .Append("=")
            .Append(ValueText.ToHtmlSafeLiteral())
            .ToString();
    }
}