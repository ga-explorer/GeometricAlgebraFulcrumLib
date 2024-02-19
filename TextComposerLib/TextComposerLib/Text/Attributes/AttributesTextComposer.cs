using System;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Text.Attributes;

/// <summary>
/// This class is a linear text composer that can also hold a list of
/// attribute-value pairs to be composed inside the final text
/// </summary>
public class AttributesTextComposer : 
    LinearTextComposer
{
    public TextAttributesDictionary AttributesDictionary { get; }
        = new TextAttributesDictionary();

    public bool ContainsNonDefaultAttributes
        => AttributesDictionary.ContainsNonDefaultAttributes;
        
    public string AttributesText 
        => AttributesDictionary.AttributesText;
        

    public override LinearTextComposer Clear()
    {
        base.Clear();

        AttributesDictionary.Clear();

        return this;
    }

    private TextAttribute GetOrAddItem(string key)
    {
        return AttributesDictionary.GetOrAddItem(key);
    }

    public AttributesTextComposer SetAttributeValue(string key, string value, string valueDefault)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.ValueDefault = valueDefault ?? string.Empty;
        item.Value = value ?? string.Empty;

        return this;
    }
        
    public AttributesTextComposer SetAttributeValue(string key, string value)
    {
        var item = GetOrAddItem(key);

        item.HideDefaultValue = true;
        item.Value = value;

        return this;
    }

    public AttributesTextComposer SetAttributeValue(string key, string value, Predicate<string> isDefaultPredicate)
    {
        if (isDefaultPredicate(value))
            RemoveAttribute(key);
        else
            SetAttributeValue(key, value);

        return this;
    }
        
    public AttributesTextComposer RemoveAttribute(string name)
    {
        AttributesDictionary.RemoveAttribute(name);

        return this;
    }


    public AttributesTextComposer AppendAttributesText()
    {
        Append(AttributesText);

        return this;
    }

    public AttributesTextComposer AppendLineAttributesText()
    {
        AppendLine(AttributesText);

        return this;
    }

    public AttributesTextComposer AppendAtNewLineAttributesText()
    {
        AppendAtNewLine(AttributesText);

        return this;
    }

    public AttributesTextComposer AppendLineAtNewLineAttributesText()
    {
        AppendLineAtNewLine(AttributesText);

        return this;
    }
}