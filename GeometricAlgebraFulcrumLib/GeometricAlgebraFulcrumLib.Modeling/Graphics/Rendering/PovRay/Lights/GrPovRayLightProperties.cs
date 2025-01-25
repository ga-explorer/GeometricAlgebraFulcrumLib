using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;

public abstract class GrPovRayLightProperties :
    GrPovRayAttributeSet
{
    public GrPovRayStringValue? FadeDistance
    {
        get => GetAttributeValueOrNull<GrPovRayStringValue>("fade_distance");
        set => SetAttributeValue("fade_distance", value);
    }

    public GrPovRayStringValue? FadePower
    {
        get => GetAttributeValueOrNull<GrPovRayStringValue>("fade_power");
        set => SetAttributeValue("fade_power", value);
    }

    public GrPovRayStringValue? LooksLike
    {
        get => GetAttributeValueOrNull<GrPovRayStringValue>("looks_like");
        set => SetAttributeValue("looks_like", value);
    }
    
    public GrPovRayBooleanValue? MediaAttenuation
    {
        get => GetAttributeValueOrNull<GrPovRayBooleanValue>("media_attenuation");
        set => SetAttributeValue("media_attenuation", value);
    }

    public GrPovRayBooleanValue? MediaInteraction
    {
        get => GetAttributeValueOrNull<GrPovRayBooleanValue>("media_interaction");
        set => SetAttributeValue("media_interaction", value);
    }

    public GrPovRayStringValue? ProjectedThrough
    {
        get => GetAttributeValueOrNull<GrPovRayStringValue>("projected_through");
        set => SetAttributeValue("projected_through", value);
    }
    
    public GrPovRayVector3Value? ParallelPointAt { get; set; }

    public bool IsParallel 
        => ParallelPointAt is not null;

    public bool Shadowless { get; set; }


    protected GrPovRayLightProperties()
    {
    }

    protected GrPovRayLightProperties(GrPovRayLightProperties properties)
    {
        SetAttributeValues(properties);
    }


    public override string GetPovRayCode()
    {
        return GetKeyValueCodePairs().Select(
            p => $"{p.Key} {p.Value}"
        ).Concatenate(Environment.NewLine);
    }

}