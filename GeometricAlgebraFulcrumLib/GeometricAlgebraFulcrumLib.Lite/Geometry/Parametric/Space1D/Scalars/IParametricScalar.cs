using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

/// <summary>
/// A parametric 1D curve with continuous first derivative
/// </summary>
public interface IParametricScalar :
    IGeometricElement
{
    Float64ScalarRange ParameterRange { get; }
    
    Float64Scalar GetValue(double parameterValue);

    Float64Scalar GetDerivative1Value(double parameterValue);
}