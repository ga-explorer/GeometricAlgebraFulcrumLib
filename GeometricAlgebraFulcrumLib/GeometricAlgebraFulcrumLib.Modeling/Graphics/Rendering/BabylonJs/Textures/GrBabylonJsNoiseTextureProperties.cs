using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsNoiseTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsFloat32Value? AnimationSpeedFactor
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationSpeedFactor");
        set => SetAttributeValue("animationSpeedFactor", value);
    }

    public GrBabylonJsFloat32Value? Brightness
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("brightness");
        set => SetAttributeValue("brightness", value);
    }

    public GrBabylonJsFloat32Value? Octaves
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("octaves");
        set => SetAttributeValue("octaves", value);
    }

    public GrBabylonJsFloat32Value? Persistence
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("persistence");
        set => SetAttributeValue("persistence", value);
    }

    public GrBabylonJsFloat32Value? Time
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("time");
        set => SetAttributeValue("time", value);
    }


    public GrBabylonJsNoiseTextureProperties()
    {
    }

    public GrBabylonJsNoiseTextureProperties(GrBabylonJsNoiseTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}