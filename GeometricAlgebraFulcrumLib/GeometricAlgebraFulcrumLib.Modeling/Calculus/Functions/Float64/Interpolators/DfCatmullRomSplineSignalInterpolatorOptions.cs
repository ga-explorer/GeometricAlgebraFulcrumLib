using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public sealed class DfCatmullRomSplineSignalInterpolatorOptions :
    DfSignalInterpolatorOptions
{
    public int BezierDegree { get; set; }
        = 3;

    public CatmullRomSplineType SplineType { get; set; }
        = CatmullRomSplineType.Centripetal;
}