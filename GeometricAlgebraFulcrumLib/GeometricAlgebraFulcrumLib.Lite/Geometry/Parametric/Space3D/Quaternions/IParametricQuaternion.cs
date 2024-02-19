using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Quaternions;

/// <summary>
/// A parametric 4D curve with continuous first derivative
/// </summary>
public interface IParametricQuaternion :
    IGeometricElement
{
    Float64ScalarRange ParameterRange { get; }

    Float64Quaternion GetQuaternion(double parameterValue);

    Float64Quaternion GetDerivative1Quaternion(double parameterValue);
}