using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector5Value :
    GrPovRayValue<IQuint<Float64Scalar>>,
    IGrPovRayRValue
{
    internal static GrPovRayVector5Value Create(IQuint<Float64Scalar> value)
    {
        return new GrPovRayVector5Value(value);
    }


    public static implicit operator GrPovRayVector5Value(string valueText)
    {
        return new GrPovRayVector5Value(valueText);
    }

    //public static implicit operator GrPovRayVector5Value(LinFloat64Vector4D value)
    //{
    //    return new GrPovRayVector5Value(value);
    //}


    private GrPovRayVector5Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector5Value(IQuint<Float64Scalar> value)
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