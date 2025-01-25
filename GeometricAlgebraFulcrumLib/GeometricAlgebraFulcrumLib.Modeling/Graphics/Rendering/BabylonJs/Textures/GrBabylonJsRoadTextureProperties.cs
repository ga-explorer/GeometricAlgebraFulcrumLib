using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsRoadTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsColor4Value? RoadColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("roadColor");
        set => SetAttributeValue("isEnabled", value);
    }


    public GrBabylonJsRoadTextureProperties()
    {
    }

    public GrBabylonJsRoadTextureProperties(GrBabylonJsRoadTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}