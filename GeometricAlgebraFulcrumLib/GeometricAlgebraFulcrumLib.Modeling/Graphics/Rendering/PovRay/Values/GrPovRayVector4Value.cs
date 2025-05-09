using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector4Value :
    GrPovRayValue<IQuad<Float64Scalar>>,
    IGrPovRayRValue
{
    internal static GrPovRayVector4Value Create(IQuad<Float64Scalar> value)
    {
        return new GrPovRayVector4Value(value);
    }


    public static implicit operator GrPovRayVector4Value(string valueText)
    {
        return new GrPovRayVector4Value(valueText);
    }

    public static implicit operator GrPovRayVector4Value(LinFloat64Vector4D value)
    {
        return new GrPovRayVector4Value(value);
    }


    private GrPovRayVector4Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector4Value(IQuad<Float64Scalar> value)
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