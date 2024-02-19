namespace CodeComposerLib.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mtext
/// </summary>
public sealed class MathMlText : MathMlTextTokenElement
{
    public static MathMlText Create()
    {
        return new MathMlText();
    }

    public static MathMlText Create(string text)
    {
        return new MathMlText()
        {
            Text = text
        };
    }


    public override string XmlTagName 
        => "mtext";


    internal MathMlText()
    {
    }
}