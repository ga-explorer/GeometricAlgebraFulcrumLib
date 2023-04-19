using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    /// <summary>
    /// A parametric 3D curve with continuous first derivative
    /// </summary>
    public interface IParametricCurve3D :
        IGeometricElement
    {
        Float64Tuple3D GetPoint(double parameterValue);

        Float64Tuple3D GetDerivative1Point(double parameterValue);

        ParametricCurveLocalFrame3D GetFrame(double parameterValue);
    }
}