using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

public static class ParametricAngleComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IFloat64ParametricScalar curve, Func<double, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IParametricPolarAngle curve, Func<LinFloat64PolarAngle, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IFloat64ParametricCurve2D curve, Func<LinFloat64Vector2D, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IParametricCurve3D curve, Func<LinFloat64Vector3D, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetBivector(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IParametricCurve4D curve, Func<LinFloat64Vector4D, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricPolarAngle ToParametricAngle(this IParametricQuaternion curve, Func<LinFloat64Quaternion, LinFloat64PolarAngle> vectorMapping)
    {
        return ComputedParametricPolarAngle.Create(
            curve.ParameterRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }

    
        

}