using CodeComposerLib.MathML.Constants;

namespace CodeComposerLib.MathML.Values.Size;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/CSS/length
/// </summary>
public sealed class MathMlAbsoluteLengthValue : MathMlLengthValue
{
    public double Value { get; set; }
        = 1;

    public MathMlAbsoluteLengthUnit Unit { get; set; }
        = MathMlAbsoluteLengthUnit.Point;


    public override string ValueText
        => Value.ToString("G") + " " + Unit.GetName();


    internal MathMlAbsoluteLengthValue()
    {
    }
}