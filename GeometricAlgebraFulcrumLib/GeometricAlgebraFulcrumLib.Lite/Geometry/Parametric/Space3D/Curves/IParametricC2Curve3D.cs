using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves
{
    /// <summary>
    /// A parametric 3D curve with continuous first and second derivatives
    /// </summary>
    public interface IParametricC2Curve3D :
        IParametricCurve3D
    {
        Float64Vector3D GetDerivative2Point(double parameterValue);
    }
}