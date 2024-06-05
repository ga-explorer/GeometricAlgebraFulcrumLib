namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

public sealed class MathMlNamedColorValue : MathMlColorValue
{
    public string ValueName { get; }

    public override string ValueText 
        => ValueName;

    internal MathMlNamedColorValue(string valueName)
    {
        ValueName = valueName;
    }

}