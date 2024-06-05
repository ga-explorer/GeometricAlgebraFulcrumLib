using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;

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