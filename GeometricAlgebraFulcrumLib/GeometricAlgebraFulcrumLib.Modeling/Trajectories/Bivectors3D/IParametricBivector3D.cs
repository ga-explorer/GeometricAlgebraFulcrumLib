using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;

/// <summary>
/// A parametric 3D bivector with continuous first derivative
/// </summary>
public interface IParametricBivector3D :
    IAlgebraicElement
{
    Float64ScalarRange TimeRange { get; }

    LinFloat64Bivector3D GetValue(double parameterValue);

    Float64Path3D GetNormalVectorCurve(LinFloat64Vector3D? zeroNormal = null);
}