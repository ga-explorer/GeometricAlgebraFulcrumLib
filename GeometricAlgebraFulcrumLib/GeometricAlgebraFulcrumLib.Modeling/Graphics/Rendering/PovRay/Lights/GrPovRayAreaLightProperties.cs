using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public sealed class GrPovRayAreaLightProperties :
    GrPovRayLightProperties
{
    public GrPovRayFloat32Value? Adaptive
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("adaptive");
        set => SetAttributeValue("adaptive", value);
    }

    public GrPovRayBooleanValue? AreaIllumination
    {
        get => GetAttributeValueOrNull<GrPovRayBooleanValue>("area_illumination");
        set => SetAttributeValue("area_illumination", value);
    }
        
    public GrPovRayFlagValue? Jitter
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("jitter");
        set => SetAttributeValue("jitter", value);
    }
        
    public GrPovRayFlagValue? Circular
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("circular");
        set => SetAttributeValue("circular", value);
    }

    public GrPovRayFlagValue? Orient
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("orient");
        set => SetAttributeValue("orient", value);
    }


    public GrPovRayAreaLightProperties()
    {
    }

    public GrPovRayAreaLightProperties(GrPovRayAreaLightProperties properties)
    {
        SetAttributeValues(properties);
    }
}