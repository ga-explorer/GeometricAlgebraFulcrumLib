using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Circles
{
    public interface IGraphicsParametricCircle2D :
        IParametricC2Curve2D,
        IArcLengthCurve2D
    {
        public double Radius { get; }

        public Float64Vector2D Center { get; }
    }
}