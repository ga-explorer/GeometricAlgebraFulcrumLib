using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRaySamplingMethodValue :
    GrPovRayValue<GrPovRaySamplingMethod>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRaySamplingMethodValue(string valueText)
    {
        return new GrPovRaySamplingMethodValue(valueText);
    }

    public static implicit operator GrPovRaySamplingMethodValue(GrPovRaySamplingMethod value)
    {
        return new GrPovRaySamplingMethodValue(value);
    }
    

    private GrPovRaySamplingMethodValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRaySamplingMethodValue(GrPovRaySamplingMethod value)
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