using System.Collections.Generic;
using CodeComposerLib.MathML.Elements;
using CodeComposerLib.MathML.Elements.Layout;
using CodeComposerLib.MathML.Elements.Tokens;
using DataStructuresLib;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.MathML.Composers;

public sealed class MathMlCodeComposer 
    : IDynamicTreeVisitor<IMathMlElement>
{
    public static string ComposeCode(IMathMlElement element, bool useSingleLine = false)
    {
        var composer = new MathMlCodeComposer()
        {
            UseSingleLine = useSingleLine
        };

        element
            .ToMathMlMath()
            .AcceptVisitor(composer);

        return composer
            ._codeComposer
            .ToString();
    }

    public static string ComposeCode(IEnumerable<IMathMlElement> elementsList, bool useSingleLine = false)
    {
        var composer = new MathMlCodeComposer()
        {
            UseSingleLine = useSingleLine
        };

        elementsList
            .ToMathMlMath()
            .AcceptVisitor(composer);

        return composer
            ._codeComposer
            .ToString();
    }


    private readonly LinearTextComposer _codeComposer 
        = new LinearTextComposer()
        {
            IndentationDefault = "  "
        };

    private readonly MathMlAttributesComposer _attrComposer
        = new MathMlAttributesComposer();


    public bool UseExceptions => true;

    public bool IgnoreNullElements => false;

    public bool UseSingleLine { get; set; }


    private MathMlCodeComposer()
    {
    }


    public void Fallback(IMathMlElement item, RuntimeBinderException excException)
    {
        throw excException;
    }


    public void Visit(MathMlNonTextTokenElement element)
    {
        _attrComposer.Clear();

        element.UpdateAttributesComposer(_attrComposer);

        if (UseSingleLine)
            _codeComposer.Append("<");
        else
            _codeComposer.AppendAtNewLine("<");

        _codeComposer.Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        _codeComposer.Append("/>");
    }

    public void Visit(MathMlTextTokenElement element)
    {
        _attrComposer.Clear();

        element.UpdateAttributesComposer(_attrComposer);

        if (UseSingleLine)
            _codeComposer.Append("<");
        else
            _codeComposer.AppendAtNewLine("<");

        _codeComposer.Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        _codeComposer
            .Append(">")
            .Append(element.ContentsText)
            .Append("</")
            .Append(element.XmlTagName)
            .Append(">");
    }

    public void Visit(MathMlLayoutElement element)
    {
        _attrComposer.Clear();

        element.UpdateAttributesComposer(_attrComposer);

        if (UseSingleLine)
            _codeComposer.Append("<");
        else
            _codeComposer.AppendAtNewLine("<");

        _codeComposer.Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        if (UseSingleLine)
            _codeComposer.Append(">");
        else
            _codeComposer
                .AppendLine(">")
                .IncreaseIndentation();

        foreach (var childElement in element.Contents)
            childElement.AcceptVisitor(this);

        if (UseSingleLine)
            _codeComposer.Append("</");
        else
            _codeComposer
                .DecreaseIndentation()
                .AppendAtNewLine("</");
            
        _codeComposer
            .Append(element.XmlTagName)
            .Append(">");
    }

    public void Visit(MathMlMath element)
    {
        _attrComposer.Clear();

        _attrComposer.SetAttributeValue(
            "xmlns",
            "http://www.w3.org/1998/Math/MathML".DoubleQuote()
        );

        element.UpdateAttributesComposer(_attrComposer);

        if (UseSingleLine)
            _codeComposer.Append("<math ");
        else
            _codeComposer.AppendAtNewLine("<math ");

        _codeComposer.Append(_attrComposer.AttributesText);

        if (UseSingleLine)
            _codeComposer.Append(">");
        else
            _codeComposer
                .AppendLine(">")
                .IncreaseIndentation();

        foreach (var childElement in element)
            childElement.AcceptVisitor(this);

        if (UseSingleLine)
            _codeComposer.Append("</math>");
        else
            _codeComposer
                .DecreaseIndentation()
                .AppendAtNewLine("</math>");
    }
}