using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Interiors;

public sealed class GrPovRayInteriorProperties :
    GrPovRayAttributeSet
{
    public GrPovRayFloat32Value? IndexOfRefraction
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("ior");
        set => SetAttributeValue("ior", value);
    }

    public GrPovRayFloat32Value? Caustics
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("caustics");
        set => SetAttributeValue("caustics", value);
    }

    public GrPovRayFloat32Value? Dispersion
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("dispersion");
        set => SetAttributeValue("dispersion", value);
    }

    public GrPovRayInt32PairValue? DispersionSamples
    {
        get => GetAttributeValueOrNull<GrPovRayInt32PairValue>("dispersion_samples");
        set => SetAttributeValue("dispersion_samples", value);
    }

    public GrPovRayFloat32Value? FadeDistance
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("fade_distance");
        set => SetAttributeValue("fade_distance", value);
    }

    public GrPovRayFloat32Value? FadePower
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("fade_power");
        set => SetAttributeValue("fade_power", value);
    }

    public GrPovRayColorValue? FadeColor
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("fade_color");
        set => SetAttributeValue("fade_color", value);
    }


    internal GrPovRayInteriorProperties()
    {
    }

    internal GrPovRayInteriorProperties(GrPovRayInteriorProperties properties)
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