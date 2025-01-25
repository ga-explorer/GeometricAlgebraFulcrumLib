using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Interiors;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayInteriorValue :
    GrPovRayValue<GrPovRayInterior>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayInteriorValue(string valueText)
    {
        return new GrPovRayInteriorValue(valueText);
    }

    public static implicit operator GrPovRayInteriorValue(GrPovRayInterior value)
    {
        return new GrPovRayInteriorValue(value);
    }
    

    private GrPovRayInteriorValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayInteriorValue(GrPovRayInterior value)
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