using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_7_2_4
/// </summary>
public sealed class GrPovRayHexagonPattern :
    GrPovRayPattern,
    IGrPovRayColorListPigmentPattern
{
    internal static GrPovRayHexagonPattern Default { get; }
        = new GrPovRayHexagonPattern();

    internal static GrPovRayHexagonPattern Create(GrPovRayColorValue color1)
    {
        return new GrPovRayHexagonPattern()
        {
            Color1 = color1
        };
    }

    internal static GrPovRayHexagonPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2)
    {
        return new GrPovRayHexagonPattern()
        {
            Color1 = color1,
            Color2 = color2
        };
    }

    internal static GrPovRayHexagonPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3)
    {
        return new GrPovRayHexagonPattern()
        {
            Color1 = color1,
            Color2 = color2,
            Color3 = color3
        };
    }


    public GrPovRayColorValue? Color1 { get; init; }

    public GrPovRayColorValue? Color2 { get; init; }

    public GrPovRayColorValue? Color3 { get; init; }


    private GrPovRayHexagonPattern()
    {
    }


    public override string GetPovRayCode()
    {
        var composer = new StringBuilder();

        composer.Append("hexagon");

        if (Color1 is not null)
            composer.Append(" ").Append(Color1.GetPovRayCode());

        if (Color2 is not null)
            composer.Append(", ").Append(Color2.GetPovRayCode());

        if (Color3 is not null)
            composer.Append(", ").Append(Color3.GetPovRayCode());

        return composer.ToString();
    }
}