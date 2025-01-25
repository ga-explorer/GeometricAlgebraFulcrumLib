using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsGrassTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsColor4Value? GroundColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("groundColor");
        set => SetAttributeValue("groundColor", value);
    }

    public GrBabylonJsColor4ArrayValue? GrassColors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("grassColors");
        set => SetAttributeValue("grassColors", value);
    }


    public GrBabylonJsGrassTextureProperties()
    {
    }

    public GrBabylonJsGrassTextureProperties(GrBabylonJsGrassTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}