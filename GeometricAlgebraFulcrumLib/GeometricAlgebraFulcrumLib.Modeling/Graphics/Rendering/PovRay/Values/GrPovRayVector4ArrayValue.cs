using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector4ArrayValue :
    GrPovRayValue<IReadOnlyList<IQuad<Float64Scalar>>>
{
    internal static GrPovRayVector4ArrayValue Create(IReadOnlyList<IQuad<Float64Scalar>> value)
    {
        return new GrPovRayVector4ArrayValue(value);
    }


    public static implicit operator GrPovRayVector4ArrayValue(string valueText)
    {
        return new GrPovRayVector4ArrayValue(valueText);
    }

    public static implicit operator GrPovRayVector4ArrayValue(LinFloat64Vector4D[] value)
    {
        return new GrPovRayVector4ArrayValue(value);
    }


    private GrPovRayVector4ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector4ArrayValue(IReadOnlyList<IQuad<Float64Scalar>> value)
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