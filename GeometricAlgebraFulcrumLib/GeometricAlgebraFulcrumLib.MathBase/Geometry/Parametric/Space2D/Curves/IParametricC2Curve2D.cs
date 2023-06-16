using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    /// <summary>
    /// A parametric 2D curve with continuous first and second derivatives
    /// </summary>
    public interface IParametricC2Curve2D :
        IParametricCurve2D
    {
        Float64Vector2D GetDerivative2Point(double parameterValue);
    }
}