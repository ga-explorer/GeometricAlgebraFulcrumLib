using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Interpolators
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