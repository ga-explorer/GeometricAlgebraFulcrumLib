using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;

/// <summary>
/// A parametric 4D curve with continuous first derivative
/// </summary>
public interface IParametricQuaternion :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Quaternion GetQuaternion(double parameterValue);

    LinFloat64Quaternion GetDerivative1Quaternion(double parameterValue);
}