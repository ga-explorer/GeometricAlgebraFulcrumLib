using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsFireTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsFloat32Value? Time
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("time");
        set => SetAttributeValue("time", value);
    }

    public GrBabylonJsVector2Value? Speed
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("speed");
        set => SetAttributeValue("speed", value);
    }

    public GrBabylonJsVector2Value? Shift
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("shift");
        set => SetAttributeValue("shift", value);
    }

    public GrBabylonJsColor4ArrayValue? FireColors
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("fireColors");
        set => SetAttributeValue("fireColors", value);
    }


    public GrBabylonJsFireTextureProperties()
    {
    }

    public GrBabylonJsFireTextureProperties(GrBabylonJsFireTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}