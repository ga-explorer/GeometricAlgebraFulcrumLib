using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media;

public sealed class GrPovRayMediaProperties :
    GrPovRayAttributeSet
{
    public GrPovRayInt32Value? Method
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("method");
        set => SetAttributeValue("method", value);
    }

    public GrPovRayInt32Value? Intervals
    {
        get => GetAttributeValueOrNull<GrPovRayInt32Value>("intervals");
        set => SetAttributeValue("intervals", value);
    }

    public GrPovRayInt32PairValue? Samples
    {
        get => GetAttributeValueOrNull<GrPovRayInt32PairValue>("samples");
        set => SetAttributeValue("samples", value);
    }

    public GrPovRayFloat32Value? Confidence
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("confidence");
        set => SetAttributeValue("confidence", value);
    }

    public GrPovRayFloat32Value? Variance
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("variance");
        set => SetAttributeValue("variance", value);
    }

    public GrPovRayFloat32Value? Ratio
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("ratio");
        set => SetAttributeValue("ratio", value);
    }

    public GrPovRayFloat32Value? Jitter
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("jitter");
        set => SetAttributeValue("jitter", value);
    }

    public GrPovRayColorValue? Absorption 
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("absorption");
        set => SetAttributeValue("absorption", value);
    }

    public GrPovRayColorValue? Emission
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("emission");
        set => SetAttributeValue("emission", value);
    }

    public GrPovRayFloat32Value? AaThreshold
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("aa_threshold");
        set => SetAttributeValue("aa_threshold", value);
    }

    public GrPovRayFloat32Value? AaLevel
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("aa_level");
        set => SetAttributeValue("aa_level", value);
    }
    
    public GrPovRayMediaScatteringValue? Scattering
    {
        get => GetAttributeValueOrNull<GrPovRayMediaScatteringValue>("scattering");
        set => SetAttributeValue("scattering", value);
    }


    internal GrPovRayMediaProperties()
    {
    }

    internal GrPovRayMediaProperties(GrPovRayMediaProperties properties)
    {
        SetAttributeValues(properties);
    }


    public override string GetPovRayCode()
    {
        return GetAttributeValueCode(
            (key, value) => key + " " + value
        ).Concatenate(Environment.NewLine);
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}