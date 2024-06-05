namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;

public sealed class MathMlNamedSizeValue : MathMlLengthValue
{
    public string ValueName { get; }

    public override string ValueText 
        => ValueName;

    internal MathMlNamedSizeValue(string valueName)
    {
        ValueName = valueName;
    }

}