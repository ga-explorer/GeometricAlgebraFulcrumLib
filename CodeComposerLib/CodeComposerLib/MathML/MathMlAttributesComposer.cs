using CodeComposerLib.MathML.Constants;
using CodeComposerLib.MathML.Values;
using TextComposerLib;
using TextComposerLib.Text.Attributes;

namespace CodeComposerLib.MathML;

internal class MathMlAttributesComposer : 
    AttributesTextComposer
{
    public MathMlAttributesComposer()
    {
        AttributesDictionary.KeyValueSeparator = "=";
        AttributesDictionary.AttributesSeparator = " ";
    }

    public MathMlAttributesComposer SetAttributeValue(string key, double value, double valueDefault)
    {
        base.SetAttributeValue(
            key, 
            value.ToString("G").DoubleQuote(), 
            valueDefault.ToString("G").DoubleQuote()
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, bool value)
    {
        base.SetAttributeValue(
            key, 
            value ? "true".DoubleQuote() : "false".DoubleQuote()
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlBoolean value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlOperatorForm value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlTextDirection value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlTextVariant value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlDisplay value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, MathMlOverflow value)
    {
        base.SetAttributeValue(
            key, 
            value.GetName().DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer SetAttributeValue(string key, IMathMlValue value)
    {
        base.SetAttributeValue(
            key, 
            value.ValueText.DoubleQuote(),
            "\"\""
        );

        return this;
    }

    public MathMlAttributesComposer AppendOpenTagCode(string tagName)
    {
        Append("<");
        Append(tagName);

        if (ContainsNonDefaultAttributes)
        {
            Append(" ");
            Append(AttributesText);
        }

        Append(">");

        return this;
    }

    public MathMlAttributesComposer AppendCloseTagCode(string tagName)
    {
        Append("</");
        Append(tagName);
        Append(">");

        return this;
    }

    public MathMlAttributesComposer AppendTagCode(string tagName, string contents, bool singleLine = false)
    {
        if (singleLine || string.IsNullOrEmpty(contents))
        {
            AppendOpenTagCode(tagName);
            Append(contents);
            AppendCloseTagCode(tagName);

            return this;
        }

        AppendOpenTagCode(tagName);
        IncreaseIndentation();
        AppendAtNewLine(contents);
        DecreaseIndentation();
        AppendAtNewLine();
        AppendCloseTagCode(tagName);

        return this;
    }

    public MathMlAttributesComposer AppendTagCode(string tagName)
    {
        Append("<");
        Append(tagName);

        if (ContainsNonDefaultAttributes)
        {
            Append(" ");
            Append(AttributesText);
        }

        Append("/>");

        return this;
    }
}