namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space1D.Curves
{
    /// <summary>
    /// A parametric 1D curve with continuous first and second derivatives
    /// </summary>
    public interface IParametricC2Curve1D :
        IParametricCurve1D
    {
        double GetDerivative2Point(double parameterValue);
    }
}