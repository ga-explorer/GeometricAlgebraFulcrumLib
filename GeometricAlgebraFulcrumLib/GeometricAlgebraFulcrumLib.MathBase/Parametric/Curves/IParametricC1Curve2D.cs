using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    /// <summary>
    /// A parametric 2D curve with continuous first derivative
    /// </summary>
    public interface IParametricC1Curve2D :
        IGeometricElement
    {
        Float64Tuple2D GetPoint(double parameterValue);

        Float64Tuple2D GetTangent(double parameterValue);

        Float64Tuple2D GetUnitTangent(double parameterValue);

        ParametricCurveLocalFrame2D GetFrame(double parameterValue);
    }
}