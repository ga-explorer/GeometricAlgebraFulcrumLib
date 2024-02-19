using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public enum SdlLatheSplineType
{
    LinearSpline = 0, QuadraticSpline = 1, CubicSpline = 2, BezierSpline = 3
}

public class SdlLathe : SdlPolynomialObject, ISdlFspObject
{
    private static readonly string[] SdlLatheSplineTypeNames = new[]
    {
        "linear_spline", "quadratic_spline", "cubic_spline", "bezier_spline"
    };


    public SdlLatheSplineType SplineType { get; set; }

    public string SplineTypeName => SdlLatheSplineTypeNames[(int)SplineType];

    public List<ISdlVectorValue> Points { get; private set; }


    public SdlLathe()
    {
        Points = new List<ISdlVectorValue>();
    }
}