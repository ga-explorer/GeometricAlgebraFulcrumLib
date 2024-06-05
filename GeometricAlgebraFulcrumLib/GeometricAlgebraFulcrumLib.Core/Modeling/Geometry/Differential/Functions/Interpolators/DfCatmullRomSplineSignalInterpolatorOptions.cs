using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;

public sealed class DfCatmullRomSplineSignalInterpolatorOptions :
    DfSignalInterpolatorOptions
{
    public int BezierDegree { get; set; } 
        = 3;

    public CatmullRomSplineType SplineType { get; set; } 
        = CatmullRomSplineType.Centripetal;
}