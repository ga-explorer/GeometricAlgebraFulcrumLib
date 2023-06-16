using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    public interface IParametricCurve2D :
        IGeometricElement
    {
        Float64Range1D ParameterRange { get; }

        Float64Vector2D GetPoint(double t);

        Float64Vector2D GetDerivative1Point(double parameterValue);

        ParametricCurveLocalFrame2D GetFrame(double parameterValue);
    }
}