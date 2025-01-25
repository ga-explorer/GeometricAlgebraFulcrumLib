using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_7_1_13
/// </summary>
public sealed class GrPovRayGradientPattern :
    GrPovRayPattern
{
    internal static GrPovRayGradientPattern DefaultX { get; }
        = new GrPovRayGradientPattern(GrPovRayVector3Value.E1);

    internal static GrPovRayGradientPattern DefaultY { get; }
        = new GrPovRayGradientPattern(GrPovRayVector3Value.E2);

    internal static GrPovRayGradientPattern DefaultZ { get; }
        = new GrPovRayGradientPattern(GrPovRayVector3Value.E3);

    internal static GrPovRayGradientPattern Create(GrPovRayVector3Value orientation)
    {
        return new GrPovRayGradientPattern(orientation);
    }


    public GrPovRayVector3Value Orientation { get; }


    private GrPovRayGradientPattern(GrPovRayVector3Value orientation)
    {
        Orientation = orientation;
    }


    public override string GetPovRayCode()
    {
        return "gradient " + Orientation.GetPovRayCode();
    }
}