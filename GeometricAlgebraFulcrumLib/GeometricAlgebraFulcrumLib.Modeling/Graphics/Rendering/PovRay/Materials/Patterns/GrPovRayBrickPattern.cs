using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_7_1_4
/// </summary>
public sealed class GrPovRayBrickPattern :
    GrPovRayPattern,
    IGrPovRayColorListPigmentPattern
{
    internal static GrPovRayBrickPattern Default { get; }
        = new GrPovRayBrickPattern();


    public static GrPovRayBrickPattern Create(GrPovRayColorValue color1)
    {
        return new GrPovRayBrickPattern()
        {
            Color1 = color1
        };
    }

    public static GrPovRayBrickPattern Create(GrPovRayColorValue color1, GrPovRayColorValue color2)
    {
        return new GrPovRayBrickPattern()
        {
            Color1 = color1,
            Color2 = color2
        };
    }


    public GrPovRayColorValue? Color1 { get; init; }

    public GrPovRayColorValue? Color2 { get; init; }


    private GrPovRayBrickPattern()
    {
    }


    public override string GetPovRayCode()
    {
        var composer = new StringBuilder();

        composer.Append("brick");

        if (Color1 is not null)
            composer.Append(" ").Append(Color1.GetPovRayCode());

        if (Color2 is not null)
            composer.Append(", ").Append(Color2.GetPovRayCode());

        return composer.ToString();
    }
}