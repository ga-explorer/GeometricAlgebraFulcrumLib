using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Objects.FSP;

public enum SdlPrismInterpolationKind
{
    LinearSpline = 0, QuadraticSpline = 1, CubicSpline = 2, BezierSpline = 3
}

public enum SdlPrismSweepKind
{
    LinearSweep = 0, ConicSweep
}

public class SdlPrism : SdlPolynomialObject, ISdlFspObject
{
    private static readonly string[] InterpolationKindNames = new[]
    {
        "linear_spline", "quadratic_spline", "cubic_spline", "bezier_spline"
    };

    private static readonly string[] SweepKindNames = new[]
    {
        "linear_sweep", "conic_sweep"
    };


    public ISdlScalarValue Height1 { get; set; }

    public ISdlScalarValue Height2 { get; set; }

    public List<ISdlVectorValue> Points { get; private set; }

    public bool Open { get; set; }

    public SdlPrismInterpolationKind InterpolationKind { get; set; }

    public string InterpolationKindName => InterpolationKindNames[(int)InterpolationKind];

    public SdlPrismSweepKind SweepKind { get; set; }

    public string SweepKindName => SweepKindNames[(int)SweepKind];


    public SdlPrism()
    {
        Points = new List<ISdlVectorValue>();
    }

}