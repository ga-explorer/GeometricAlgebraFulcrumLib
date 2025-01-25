using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsCloudTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsColor4Value? SkyColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("skyColor");
        set => SetAttributeValue("skyColor", value);
    }

    public GrBabylonJsColor4Value? CloudColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("cloudColor");
        set => SetAttributeValue("cloudColor", value);
    }


    public GrBabylonJsCloudTextureProperties()
    {
    }

    public GrBabylonJsCloudTextureProperties(GrBabylonJsCloudTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}