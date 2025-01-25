using System.Text;
using CSharpMath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_6_3
/// </summary>
public sealed class GrPovRayFinishProperties :
    GrPovRayAttributeSet
{
    public GrPovRayFinishReflectionProperties? Reflection { get; set; }

    public GrPovRayColorValue? AmbientColor
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("ambient");
        set => SetAttributeValue("ambient", value);
    }

    public GrPovRayColorValue? EmissionColor
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("emission");
        set => SetAttributeValue("emission", value);
    }

    public GrPovRayColorValue? ReflectionColor
    {
        get => GetAttributeValueOrNull<GrPovRayColorValue>("reflection");
        set => SetAttributeValue("reflection", value);
    }

    public GrPovRayFloat32Value? BrillianceAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("brilliance");
        set => SetAttributeValue("brilliance", value);
    }

    public GrPovRayFloat32Value? RoughnessAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("roughness");
        set => SetAttributeValue("roughness", value);
    }

    public GrPovRayFloat32Value? GraininessAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("crand");
        set => SetAttributeValue("crand", value);
    }

    public GrPovRayFlagValue? Metallic
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("metallic");
        set => SetAttributeValue("metallic", value);
    }

    public GrPovRayFlagValue? ConserveEnergy
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("conserve_energy");
        set => SetAttributeValue("conserve_energy", value);
    }

    public GrPovRayFlagValue? DiffuseAlbedo
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("diffuseAlbedo");
        set => SetAttributeValue("diffuseAlbedo", value);
    }

    public GrPovRayFloat32Value? DiffuseAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("diffuse");
        set => SetAttributeValue("diffuse", value);
    }

    public GrPovRayFloat32Value? DiffuseAmountBack
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("diffuse1");
        set => SetAttributeValue("diffuse1", value);
    }

    public GrPovRayFlagValue? PhongAlbedo
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("phongAlbedo");
        set => SetAttributeValue("phongAlbedo", value);
    }

    public GrPovRayFloat32Value? PhongAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("phong");
        set => SetAttributeValue("phong", value);
    }

    public GrPovRayFloat32Value? PhongSizeAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("phong_size");
        set => SetAttributeValue("phong_size", value);
    }

    public GrPovRayFlagValue? SpecularHighlightAlbedo
    {
        get => GetAttributeValueOrNull<GrPovRayFlagValue>("specularAlbedo");
        set => SetAttributeValue("specularAlbedo", value);
    }

    public GrPovRayFloat32Value? SpecularHighlightAmount
    {
        get => GetAttributeValueOrNull<GrPovRayFloat32Value>("specular");
        set => SetAttributeValue("specular", value);
    }


    internal GrPovRayFinishProperties()
    {
    }

    internal GrPovRayFinishProperties(GrPovRayFinishProperties properties)
    {
        SetAttributeValues(properties);
    }


    public override string GetPovRayCode()
    {
        var codeList = GetAttributeValueCode(
            (key, value) => key + " " + value,
            "ambient",
            "emission",
            "reflection",
            "brilliance",
            "roughness",
            "crand",
            "metallic",
            "conserve_energy",
            "phong_size"
        ).ToList();

        if (TryGetAttributeValue("diffuse", out var v1))
        {
            var composer = new StringBuilder();

            composer.Append("diffuse ");

            if (TryGetAttributeValue<GrPovRayFlagValue>("diffuseAlbedo", out var v2) && v2.Value.IsTrue)
                composer.Append("albedo ");

            composer.AppendLine(v1.GetAttributeValueCode());

            if (TryGetAttributeValue("diffuse1", out var v3))
                composer.Append(", ").Append(v3.GetAttributeValueCode());

            codeList.Add(composer.ToString());
        }

        if (TryGetAttributeValue("phong", out var v4))
        {
            var composer = new StringBuilder();

            composer.Append("phong ");

            if (TryGetAttributeValue<GrPovRayFlagValue>("phongAlbedo", out var v2) && v2.Value.IsTrue)
                composer.Append("albedo");

            composer.AppendLine(v4.GetAttributeValueCode());

            codeList.Add(composer.ToString());
        }

        if (TryGetAttributeValue("specular", out var v5))
        {
            var composer = new StringBuilder();

            composer.Append("specular ");

            if (TryGetAttributeValue<GrPovRayFlagValue>("specularAlbedo", out var v2) && v2.Value.IsTrue)
                composer.Append("albedo");

            composer.AppendLine(v5.GetAttributeValueCode());

            codeList.Add(composer.ToString());
        }

        if (Reflection is not null && !Reflection.IsEmpty())
            codeList.Add(Reflection.GetPovRayCode());

        return codeList.Concatenate(Environment.NewLine);
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}