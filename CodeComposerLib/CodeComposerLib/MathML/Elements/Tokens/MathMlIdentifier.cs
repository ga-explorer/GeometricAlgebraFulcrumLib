namespace CodeComposerLib.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mi
/// </summary>
public sealed class MathMlIdentifier : MathMlTextTokenElement
{
    public static MathMlIdentifier Create()
    {
        return new MathMlIdentifier();
    }

    public static MathMlIdentifier Create(string text)
    {
        return new MathMlIdentifier(){Text = text};
    }


    public override string XmlTagName => "mi";


    internal MathMlIdentifier()
    {
    }
}