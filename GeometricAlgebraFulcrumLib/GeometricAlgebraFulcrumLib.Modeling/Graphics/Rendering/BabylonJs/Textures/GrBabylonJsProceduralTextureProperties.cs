using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public abstract class GrBabylonJsProceduralTextureProperties :
    GrBabylonJsBaseTextureProperties
{
    public GrBabylonJsBooleanValue? IsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
        set => SetAttributeValue("isEnabled", value);
    }

    public GrBabylonJsBooleanValue? Animate
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("animate");
        set => SetAttributeValue("animate", value);
    }

    public GrBabylonJsBooleanValue? AutoClear
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClear");
        set => SetAttributeValue("autoClear", value);
    }
        
    public GrBabylonJsFloat32Value? RefreshRate
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("refreshRate");
        set => SetAttributeValue("refreshRate", value);
    }

        
    protected GrBabylonJsProceduralTextureProperties()
    {
    }

    protected GrBabylonJsProceduralTextureProperties(GrBabylonJsProceduralTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}