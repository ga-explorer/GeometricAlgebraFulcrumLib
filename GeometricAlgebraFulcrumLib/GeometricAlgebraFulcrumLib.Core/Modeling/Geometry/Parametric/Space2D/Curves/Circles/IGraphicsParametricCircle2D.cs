using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Circles;

public interface IGraphicsParametricCircle2D :
    IParametricC2Curve2D,
    IArcLengthCurve2D
{
    public double Radius { get; }

    public LinFloat64Vector2D Center { get; }
}