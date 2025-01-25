using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;

public sealed class GrPovRayFinishReflectionProperties :
    GrPovRayAttributeSet
{
    public GrPovRayColorValue? ReflectionMinColor { get; }

    public GrPovRayColorValue ReflectionMaxColor { get; }

    public GrPovRayBooleanValue? Fresnel
    {
        get => GetAttributeValueOrNull<GrPovRayBooleanValue>("fresnel");
        set => SetAttributeValue("fresnel", value);
    }
    
    public GrPovRayFloat32Value? Falloff
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("falloff");
        set => SetAttributeValue("falloff", value);
    }
    
    public GrPovRayFloat32Value? Exponent
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("exponent");
        set => SetAttributeValue("exponent", value);
    }
    
    public GrPovRayFloat32Value? Metallic
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("metallic");
        set => SetAttributeValue("metallic", value);
    }

    internal GrPovRayFinishReflectionProperties(GrPovRayColorValue reflectionMaxColor)
    {
        ReflectionMinColor = null;
        ReflectionMaxColor = reflectionMaxColor;
    }
    
    internal GrPovRayFinishReflectionProperties(GrPovRayColorValue reflectionMinColor, GrPovRayColorValue reflectionMaxColor)
    {
        ReflectionMinColor = reflectionMinColor;
        ReflectionMaxColor = reflectionMaxColor;
    }

    internal GrPovRayFinishReflectionProperties(GrPovRayFinishReflectionProperties properties)
    {
        ReflectionMinColor = properties.ReflectionMinColor;
        ReflectionMaxColor = properties.ReflectionMaxColor;

        SetAttributeValues(properties);
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer.Append("reflection { ");

        if (ReflectionMinColor is not null)
            composer.Append(ReflectionMinColor.GetPovRayCode()).Append(", ");

        composer.Append(ReflectionMaxColor.GetPovRayCode()).Append(" ");

        var code = GetAttributeValueCode(
            (key, value) => key + " " + value
        ).Concatenate(" ", "", " ");

        composer
            .Append(code)
            .Append("}");

        return composer.ToString();
    }
}