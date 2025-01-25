using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public sealed class GrPovRayGlobalSettingsProperties :
    GrPovRayAttributeSet
{
    public GrPovRayFloat32Value? AdaptiveDepthControlBailout
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("adc_bailout");
        set => SetAttributeValue("adc_bailout", value);
    }

    public GrPovRayColorValue? AmbientLight
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("ambient_light");
        set => SetAttributeValue("ambient_light", value);
    }

    public GrPovRayFloat32Value? AssumedGamma
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("assumed_gamma");
        set => SetAttributeValue("assumed_gamma", value);
    }
    
    public GrPovRayColorValue? IridescenceWavelength
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("irid_wavelength");
        set => SetAttributeValue("irid_wavelength", value);
    }

    public GrPovRayInt32Value? MaxTraceLevel
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("max_trace_level");
        set => SetAttributeValue("max_trace_level", value);
    }

    public GrPovRayInt32Value? MaxIntersections
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("max_intersections");
        set => SetAttributeValue("max_intersections", value);
    }
        
    public GrPovRayFloat32Value? MmPerUnit
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("mm_per_unit");
        set => SetAttributeValue("mm_per_unit", value);
    }
        
    public GrPovRayInt32Value? NumberOfWaves
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("number_of_waves");
        set => SetAttributeValue("number_of_waves", value);
    }
        

    public GrPovRayGlobalSettingsProperties()
    {
        AssumedGamma = GrPovRayFloat32Value.One;
    }

    public GrPovRayGlobalSettingsProperties(GrPovRayGlobalSettingsProperties properties)
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