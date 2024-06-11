using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;

/// <summary>
/// A parametric 3D bivector with continuous first derivative
/// </summary>
public interface IParametricBivector3D :
    IAlgebraicElement
{
    Float64ScalarRange ParameterRange { get; }

    LinFloat64Bivector3D GetBivector(double parameterValue);

    IParametricCurve3D GetNormalVectorCurve(LinFloat64Vector3D? zeroNormal = null);
}