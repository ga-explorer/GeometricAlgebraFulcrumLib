using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves
{
    public interface IParametricCurveLocalFrame2D :
        IFloat64Vector2D
    {
        int Index { get; }

        Float64Vector2D Point { get; }

        Color Color { get; set; }

        Float64Scalar ParameterValue { get; }

        Float64Vector2D Tangent { get; }

        Float64Vector2D Normal { get; }
    }
}