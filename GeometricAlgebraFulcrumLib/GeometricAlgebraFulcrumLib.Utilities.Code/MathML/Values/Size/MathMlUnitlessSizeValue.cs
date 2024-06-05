namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;

public sealed class MathMlUnitlessSizeValue : MathMlLengthValue
{
    public double Value { get; set; }
        = 1;


    public override string ValueText
        => Value.ToString("G");


    internal MathMlUnitlessSizeValue()
    {
    }
}