using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayMaterialValue :
    GrPovRayValue<GrPovRayMaterial>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayMaterialValue(string valueText)
    {
        return new GrPovRayMaterialValue(valueText);
    }

    public static implicit operator GrPovRayMaterialValue(GrPovRayMaterial value)
    {
        return new GrPovRayMaterialValue(value);
    }
    

    private GrPovRayMaterialValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayMaterialValue(GrPovRayMaterial value)
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