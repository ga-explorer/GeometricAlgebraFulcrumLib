namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.TikZ;

public sealed class TikZLengthUnit
{
    public static TikZLengthUnit None { get; }
        = new TikZLengthUnit(string.Empty);

    public static TikZLengthUnit Points { get; }
        = new TikZLengthUnit("pt");

    public static TikZLengthUnit Centimeters { get; }
        = new TikZLengthUnit("cm");


    public string UnitSymbol { get; }


    private TikZLengthUnit(string unitSymbol)
    {
        UnitSymbol = unitSymbol;
    }


    public string GetLengthText(double value)
    {
        return $"{value}{UnitSymbol}";
    }
}