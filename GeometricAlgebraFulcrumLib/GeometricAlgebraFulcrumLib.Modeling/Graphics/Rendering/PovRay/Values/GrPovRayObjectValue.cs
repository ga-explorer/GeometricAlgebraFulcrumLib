using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayObjectValue :
    GrPovRayValue<GrPovRayObject>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayObjectValue(string valueText)
    {
        return new GrPovRayObjectValue(valueText);
    }

    public static implicit operator GrPovRayObjectValue(GrPovRayObject value)
    {
        return new GrPovRayObjectValue(value);
    }
    

    private GrPovRayObjectValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayObjectValue(GrPovRayObject value)
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