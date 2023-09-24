using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators
{
    public sealed class DfCatmullRomSplineSignalInterpolatorOptions :
        DfSignalInterpolatorOptions
    {
        public int BezierDegree { get; set; } 
            = 3;

        public CatmullRomSplineType SplineType { get; set; } 
            = CatmullRomSplineType.Centripetal;
    }
}