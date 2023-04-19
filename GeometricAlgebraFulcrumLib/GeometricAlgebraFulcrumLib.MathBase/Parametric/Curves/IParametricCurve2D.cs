using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    public interface IParametricCurve2D :
        IGeometricElement
    {
        Float64Tuple2D GetPoint(double t);

        Float64Tuple2D GetDerivative1Point(double parameterValue);
        
        ParametricCurveLocalFrame2D GetFrame(double parameterValue);
    }
}