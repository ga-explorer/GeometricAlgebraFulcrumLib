using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayCameraValue :
    GrPovRayValue<GrPovRayCamera>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayCameraValue(string valueText)
    {
        return new GrPovRayCameraValue(valueText);
    }

    public static implicit operator GrPovRayCameraValue(GrPovRayCamera value)
    {
        return new GrPovRayCameraValue(value);
    }
    

    private GrPovRayCameraValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayCameraValue(GrPovRayCamera value)
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