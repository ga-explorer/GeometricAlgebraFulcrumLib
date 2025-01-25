using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayTransformValue :
    GrPovRayValue<GrPovRayTransform>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayTransformValue(string valueText)
    {
        return new GrPovRayTransformValue(valueText);
    }

    public static implicit operator GrPovRayTransformValue(GrPovRayTransform value)
    {
        return new GrPovRayTransformValue(value);
    }
    

    private GrPovRayTransformValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayTransformValue(GrPovRayTransform value)
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