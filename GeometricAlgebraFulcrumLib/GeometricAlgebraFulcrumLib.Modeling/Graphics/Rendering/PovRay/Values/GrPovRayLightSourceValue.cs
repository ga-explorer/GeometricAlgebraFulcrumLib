using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayLightSourceValue :
    GrPovRayValue<GrPovRayLightSource>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayLightSourceValue(string valueText)
    {
        return new GrPovRayLightSourceValue(valueText);
    }

    public static implicit operator GrPovRayLightSourceValue(GrPovRayLightSource value)
    {
        return new GrPovRayLightSourceValue(value);
    }
    

    private GrPovRayLightSourceValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayLightSourceValue(GrPovRayLightSource value)
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