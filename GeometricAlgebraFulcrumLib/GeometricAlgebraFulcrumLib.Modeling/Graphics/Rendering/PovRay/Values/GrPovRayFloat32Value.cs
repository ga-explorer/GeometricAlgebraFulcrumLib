using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayFloat32Value :
    GrPovRayValue<float>,
    IGrPovRayRValue
{
    public static GrPovRayFloat32Value Zero { get; } 
        = new GrPovRayFloat32Value(0);

    public static GrPovRayFloat32Value One { get; } 
        = new GrPovRayFloat32Value(1);


    public static implicit operator GrPovRayFloat32Value(string valueText)
    {
        return new GrPovRayFloat32Value(valueText);
    }

    public static implicit operator GrPovRayFloat32Value(float value)
    {
        return new GrPovRayFloat32Value(value);
    }

    public static implicit operator GrPovRayFloat32Value(double value)
    {
        return new GrPovRayFloat32Value((float) value);
    }
    
    public static implicit operator GrPovRayFloat32Value(Float64Scalar value)
    {
        return new GrPovRayFloat32Value((float) value);
    }


    private GrPovRayFloat32Value(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayFloat32Value(float value)
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