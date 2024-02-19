using CodeComposerLib.MathML.Constants;

namespace CodeComposerLib.MathML.Values.Size;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/CSS/length
/// </summary>
public sealed class MathMlPercentageLengthValue : MathMlLengthValue
{
    public double Value { get; set; }
        = 1;

    public MathMlPercentageLengthUnit Unit { get; set; }
        = MathMlPercentageLengthUnit.ViewportWidth1P;


    public override string ValueText
        => Value.ToString("G") + " " + Unit.GetName();


    internal MathMlPercentageLengthValue()
    {
    }
}