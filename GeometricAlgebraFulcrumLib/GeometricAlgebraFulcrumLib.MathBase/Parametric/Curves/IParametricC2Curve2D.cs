using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    /// <summary>
    /// A parametric 2D curve with continuous first and second derivatives
    /// </summary>
    public interface IParametricC2Curve2D :
        IParametricC1Curve2D
    {
        Float64Tuple2D GetDerivative2Point(double parameterValue);
    }
}