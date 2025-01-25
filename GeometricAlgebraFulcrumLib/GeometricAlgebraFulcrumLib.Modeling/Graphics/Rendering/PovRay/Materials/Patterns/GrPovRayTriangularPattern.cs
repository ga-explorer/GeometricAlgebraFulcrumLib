using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_7_2_7
/// </summary>
public sealed class GrPovRayTriangularPattern :
    GrPovRayPattern
{
    internal static GrPovRayTriangularPattern Default { get; }
        = new GrPovRayTriangularPattern();

    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1
        };
    }

    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1,
            Color2 = color2
        };
    }

    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1,
            Color2 = color2,
            Color3 = color3
        };
    }
    
    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1,
            Color2 = color2,
            Color3 = color3,
            Color4 = color4
        };
    }

    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4, GrPovRayColorValue color5)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1,
            Color2 = color2,
            Color3 = color3,
            Color4 = color4,
            Color5 = color5
        };
    }

    internal static GrPovRayTriangularPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4, GrPovRayColorValue color5, GrPovRayColorValue color6)
    {
        return new GrPovRayTriangularPattern()
        {
            Color1 = color1,
            Color2 = color2,
            Color3 = color3,
            Color4 = color4,
            Color5 = color5,
            Color6 = color6
        };
    }


    public GrPovRayColorValue? Color1 { get; init; }

    public GrPovRayColorValue? Color2 { get; init; }

    public GrPovRayColorValue? Color3 { get; init; }

    public GrPovRayColorValue? Color4 { get; init; }

    public GrPovRayColorValue? Color5 { get; init; }

    public GrPovRayColorValue? Color6 { get; init; }


    private GrPovRayTriangularPattern()
    {
    }


    public override bool IsEmptyCodeElement()
    {
        return false;
    }

    public override string GetPovRayCode()
    {
        var composer = new StringBuilder();

        composer.Append("triangular");

        if (Color1 is not null)
            composer.Append(" ").Append(Color1.GetPovRayCode());

        if (Color2 is not null)
            composer.Append(", ").Append(Color2.GetPovRayCode());

        if (Color3 is not null)
            composer.Append(", ").Append(Color3.GetPovRayCode());

        if (Color4 is not null)
            composer.Append(", ").Append(Color4.GetPovRayCode());

        if (Color5 is not null)
            composer.Append(", ").Append(Color5.GetPovRayCode());

        if (Color6 is not null)
            composer.Append(", ").Append(Color6.GetPovRayCode());

        return composer.ToString();
    }
}