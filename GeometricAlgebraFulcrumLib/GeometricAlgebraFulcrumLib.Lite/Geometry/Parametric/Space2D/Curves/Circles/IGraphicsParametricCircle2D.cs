using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Circles;

public interface IGraphicsParametricCircle2D :
    IParametricC2Curve2D,
    IArcLengthCurve2D
{
    public double Radius { get; }

    public Float64Vector2D Center { get; }
}