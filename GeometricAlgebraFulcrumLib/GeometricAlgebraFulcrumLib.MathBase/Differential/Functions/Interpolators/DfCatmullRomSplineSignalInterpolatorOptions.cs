using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.CatmullRom;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators
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