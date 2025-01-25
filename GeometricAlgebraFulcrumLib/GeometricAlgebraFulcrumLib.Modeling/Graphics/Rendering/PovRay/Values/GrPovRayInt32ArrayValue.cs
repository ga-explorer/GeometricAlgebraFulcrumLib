namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayInt32ArrayValue :
    GrPovRayValue<IReadOnlyList<int>>
{
    internal static GrPovRayInt32ArrayValue Create(IReadOnlyList<int> value)
    {
        return new GrPovRayInt32ArrayValue(value);
    }


    public static implicit operator GrPovRayInt32ArrayValue(string valueText)
    {
        return new GrPovRayInt32ArrayValue(valueText);
    }

    public static implicit operator GrPovRayInt32ArrayValue(int[] value)
    {
        return new GrPovRayInt32ArrayValue(value);
    }
    

    private GrPovRayInt32ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayInt32ArrayValue(IReadOnlyList<int> value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}