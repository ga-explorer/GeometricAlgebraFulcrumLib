using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayTextureValue :
    GrPovRayValue<GrPovRayTexture>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayTextureValue(string valueText)
    {
        return new GrPovRayTextureValue(valueText);
    }

    public static implicit operator GrPovRayTextureValue(GrPovRayTexture value)
    {
        return new GrPovRayTextureValue(value);
    }
    

    private GrPovRayTextureValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayTextureValue(GrPovRayTexture value)
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