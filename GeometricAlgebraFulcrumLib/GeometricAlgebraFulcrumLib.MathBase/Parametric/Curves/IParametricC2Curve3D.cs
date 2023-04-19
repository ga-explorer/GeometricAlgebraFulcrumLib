using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    /// <summary>
    /// A parametric 3D curve with continuous first and second derivatives
    /// </summary>
    public interface IParametricC2Curve3D :
        IParametricCurve3D
    {
        Float64Tuple3D GetDerivative2Point(double parameterValue);
    }
}