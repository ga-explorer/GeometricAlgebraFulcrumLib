using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves.Circles;

public interface IGraphicsParametricCircle2D :
    IParametricC2Curve2D,
    IArcLengthCurve2D
{
    public double Radius { get; }

    public LinFloat64Vector2D Center { get; }
}