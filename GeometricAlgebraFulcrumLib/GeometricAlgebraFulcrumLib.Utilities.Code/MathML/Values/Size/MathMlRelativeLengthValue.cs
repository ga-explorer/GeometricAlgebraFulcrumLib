using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Constants;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/CSS/length
/// </summary>
public sealed class MathMlRelativeLengthValue : MathMlLengthValue
{
    public double Value { get; set; }
        = 1;

    public MathMlRelativeLengthUnit Unit { get; set; }
        = MathMlRelativeLengthUnit.CharacterWidth0;


    public override string ValueText
        => Value.ToString("G") + " " + Unit.GetName();


    internal MathMlRelativeLengthValue()
    {
    }
}