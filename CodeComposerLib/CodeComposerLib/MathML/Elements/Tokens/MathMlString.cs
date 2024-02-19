using TextComposerLib;

namespace CodeComposerLib.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/ms
/// </summary>
public sealed class MathMlString : MathMlTextTokenElement
{
    public static MathMlString Create()
    {
        return new MathMlString();
    }

    public static MathMlString Create(string text)
    {
        return new MathMlString()
        {
            Text = text
        };
    }


    public override string XmlTagName 
        => "ms";

    public string LeftQuote { get; set; }
        = "&quot;";

    public string RightQuote { get; set; }
        = "&quot;";


    internal MathMlString()
    {
    }


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer.SetAttributeValue(
            "lquote",
            LeftQuote.DoubleQuote(),
            "&quot;".DoubleQuote()
        );

        composer.SetAttributeValue(
            "rquote",
            RightQuote.DoubleQuote(),
            "&quot;".DoubleQuote()
        );
    }
}