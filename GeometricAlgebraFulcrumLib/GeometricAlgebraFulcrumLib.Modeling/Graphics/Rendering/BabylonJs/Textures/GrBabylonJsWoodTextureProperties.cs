using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsWoodTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsColor4Value? WoodColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("woodColor");
        set => SetAttributeValue("woodColor", value);
    }

    public GrBabylonJsVector2Value? AmpScale
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("ampScale");
        set => SetAttributeValue("ampScale", value);
    }

            
    public GrBabylonJsWoodTextureProperties()
    {
    }
            
    public GrBabylonJsWoodTextureProperties(GrBabylonJsWoodTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}