using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Bivectors;

/// <summary>
/// A parametric 2D bivector with continuous first derivative
/// </summary>
public interface IParametricBivector2D :
    IGeometricElement
{
    Float64ScalarRange ParameterRange { get; }
        
    Float64Bivector2D GetBivector(double parameterValue);

    Float64Bivector2D GetDerivative1Bivector(double parameterValue);

    IParametricScalar GetDualScalarCurve();
}