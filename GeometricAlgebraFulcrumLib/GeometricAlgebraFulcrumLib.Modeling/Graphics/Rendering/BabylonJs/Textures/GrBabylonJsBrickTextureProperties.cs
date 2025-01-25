using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsBrickTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsColor4Value? JointColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("jointColor");
        set => SetAttributeValue("jointColor", value);
    }

    public GrBabylonJsColor4Value? MarbleColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("marbleColor");
        set => SetAttributeValue("marbleColor", value);
    }

    public GrBabylonJsInt32Value? NumberOfTilesWidth
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("numberOfTilesWidth");
        set => SetAttributeValue("numberOfTilesWidth", value);
    }

    public GrBabylonJsInt32Value? NumberOfTilesHeight
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("numberOfTilesHeight");
        set => SetAttributeValue("numberOfTilesHeight", value);
    }


    public GrBabylonJsBrickTextureProperties()
    {
    }

    public GrBabylonJsBrickTextureProperties(GrBabylonJsBrickTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}