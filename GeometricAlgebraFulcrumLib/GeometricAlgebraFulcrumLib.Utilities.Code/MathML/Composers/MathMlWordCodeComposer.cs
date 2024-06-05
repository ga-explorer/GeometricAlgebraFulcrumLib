using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Microsoft.CSharp.RuntimeBinder;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Composers;

public sealed class MathMlWordCodeComposer 
    : IDynamicTreeVisitor<IMathMlElement>
{
    public static string ComposeCode(IMathMlElement element)
    {
        var composer = new MathMlWordCodeComposer();

        element
            .ToMathMlMath()
            .AcceptVisitor(composer);

        return composer
            ._codeComposer
            .ToString();
    }

    public static string ComposeCode(IEnumerable<IMathMlElement> elementsList)
    {
        var composer = new MathMlWordCodeComposer();

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


    private MathMlWordCodeComposer()
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

        _codeComposer
            .AppendAtNewLine("<mml:")
            .Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        _codeComposer
            .Append("/>");
    }

    public void Visit(MathMlTextTokenElement element)
    {
        _attrComposer.Clear();

        element.UpdateAttributesComposer(_attrComposer);

        _codeComposer
            .AppendAtNewLine("<mml:")
            .Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        _codeComposer
            .Append(">")
            .Append(element.ContentsText)
            .Append("</mml:")
            .Append(element.XmlTagName)
            .Append(">");
    }

    public void Visit(MathMlLayoutElement element)
    {
        _attrComposer.Clear();

        element.UpdateAttributesComposer(_attrComposer);

        _codeComposer
            .AppendAtNewLine("<mml:")
            .Append(element.XmlTagName);

        if (_attrComposer.ContainsNonDefaultAttributes)
        {
            _codeComposer
                .Append(" ")
                .Append(_attrComposer.AttributesText);
        }

        _codeComposer
            .AppendLine(">")
            .IncreaseIndentation();

        foreach (var childElement in element.Contents)
            childElement.AcceptVisitor(this);

        _codeComposer
            .DecreaseIndentation()
            .AppendAtNewLine("</mml:")
            .Append(element.XmlTagName)
            .Append(">");
    }

    public void Visit(MathMlMath element)
    {
        _attrComposer.Clear();

        _attrComposer.SetAttributeValue(
            "xmlns:mml",
            "http://www.w3.org/1998/Math/MathML".DoubleQuote()
        );

        _attrComposer.SetAttributeValue(
            "xmlns:m",
            "http://schemas.openxmlformats.org/officeDocument/2006/math".DoubleQuote()
        );

        element.UpdateAttributesComposer(_attrComposer);

        _codeComposer
            .AppendAtNewLine("<mml:math ")
            .Append(_attrComposer.AttributesText)
            .AppendLine(">")
            .IncreaseIndentation();

        foreach (var childElement in element)
            childElement.AcceptVisitor(this);

        _codeComposer
            .DecreaseIndentation()
            .AppendAtNewLine("</mml:math>");
    }
}