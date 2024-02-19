namespace CodeComposerLib.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mn
/// </summary>
public sealed class MathMlNumber : MathMlTextTokenElement
{
    public static MathMlNumber Create()
    {
        return new MathMlNumber();
    }

    public static MathMlNumber Create(string text)
    {
        return new MathMlNumber()
        {
            Text = text
        };
    }

    public static MathMlNumber Create(int value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber Create(long value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString()
        };
    }

    public static MathMlNumber Create(float value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString("G")
        };
    }

    public static MathMlNumber Create(double value)
    {
        return new MathMlNumber()
        {
            Text = value.ToString("G")
        };
    }


    public override string XmlTagName 
        => "mn";


    internal MathMlNumber()
    {
    }
}