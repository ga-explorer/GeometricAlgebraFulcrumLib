using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Trivectors
{
    /// <summary>
    /// A parametric 3D bivector with continuous first derivative
    /// </summary>
    public interface IParametricTrivector3D :
        IGeometricElement
    {
        Float64ScalarRange ParameterRange { get; }
        
        Float64Trivector3D GetTrivector(double parameterValue);

        Float64Trivector3D GetDerivative1Trivector(double parameterValue);

        IParametricScalar GetDualScalarCurve();
    }
}