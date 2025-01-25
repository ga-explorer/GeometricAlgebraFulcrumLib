using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public class GrPovRayObjectProperties :
    GrPovRayAttributeSet
{
    public GrPovRayObjectBoundSpecsValue? Bound
    {
        get => GetAttributeValueOrNull<GrPovRayObjectBoundSpecsValue>("bounded_by");
        set => SetAttributeValue("bounded_by", value);
    }

    public GrPovRayObjectClipSpecsValue? Clip
    {
        get => GetAttributeValueOrNull<GrPovRayObjectClipSpecsValue>("clipped_by");
        set => SetAttributeValue("clipped_by", value);
    }

    public GrPovRayFlagValue? NoShadow
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("no_shadow");
        set => SetAttributeValue("no_shadow", value);
    }

    public GrPovRayFlagValue? NoImage
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("no_image");
        set => SetAttributeValue("no_image", value);
    }

    public GrPovRayFlagValue? NoRadiosity
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("no_radiosity");
        set => SetAttributeValue("no_radiosity", value);
    }

    public GrPovRayFlagValue? NoReflection
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("no_reflection");
        set => SetAttributeValue("no_reflection", value);
    }

    public GrPovRayFlagValue? Inverse
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("inverse");
        set => SetAttributeValue("inverse", value);
    }

    public GrPovRayFlagValue? Hierarchy
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("hierarchy");
        set => SetAttributeValue("hierarchy", value);
    }

    public GrPovRayFlagValue? DoubleIlluminate
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("double_illuminate");
        set => SetAttributeValue("double_illuminate", value);
    }

    public GrPovRayFlagValue? Hollow
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("hollow");
        set => SetAttributeValue("hollow", value);
    }

    //public GrPovRayFlagValue? Sturm
    //{
    //    get => GetAttributeValueOrNull<GrPovRayFlagValue>("sturm");
    //    set => SetAttributeValue("sturm", value);
    //}


    internal GrPovRayObjectProperties()
    {
        NoShadow = GrPovRayFlagValue.True;
    }

    internal GrPovRayObjectProperties(GrPovRayObjectProperties properties)
    {
        SetAttributeValues(properties!);
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