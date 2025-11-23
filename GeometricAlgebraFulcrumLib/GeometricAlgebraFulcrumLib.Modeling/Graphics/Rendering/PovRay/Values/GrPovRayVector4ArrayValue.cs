using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector4ArrayValue :
    GrPovRayValue<IReadOnlyList<IQuad<double>>>
{
    internal static GrPovRayVector4ArrayValue Create(IReadOnlyList<IQuad<double>> value)
    {
        return new GrPovRayVector4ArrayValue(value);
    }


    public static implicit operator GrPovRayVector4ArrayValue(string valueText)
    {
        return new GrPovRayVector4ArrayValue(valueText);
    }

    public static implicit operator GrPovRayVector4ArrayValue(IQuad<double>[] value)
    {
        return new GrPovRayVector4ArrayValue(value);
    }


    private GrPovRayVector4ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector4ArrayValue(IReadOnlyList<IQuad<double>> value)
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