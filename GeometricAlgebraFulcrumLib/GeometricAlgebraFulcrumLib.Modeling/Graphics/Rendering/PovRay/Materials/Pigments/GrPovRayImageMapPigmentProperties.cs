using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class GrPovRayImageMapPigmentProperties :
    GrPovRayAttributeSet
{
    public GrPovRayImageMapTypeValue? MapType
    {
        get => GetAttributeValueOrNull<GrPovRayImageMapTypeValue>("map_type");
        set => SetAttributeValue("map_type", value);
    }
    
    public GrPovRayFlagValue? Once
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("once");
        set => SetAttributeValue("once", value);
    }


    internal GrPovRayImageMapPigmentProperties()
    {
    }

    internal GrPovRayImageMapPigmentProperties(GrPovRayImageMapPigmentProperties properties)
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