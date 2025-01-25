using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayPigmentValue :
    GrPovRayValue<GrPovRayPigment>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayPigmentValue(string valueText)
    {
        return new GrPovRayPigmentValue(valueText);
    }

    public static implicit operator GrPovRayPigmentValue(GrPovRayPigment value)
    {
        return new GrPovRayPigmentValue(value);
    }
    

    private GrPovRayPigmentValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayPigmentValue(GrPovRayPigment value)
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