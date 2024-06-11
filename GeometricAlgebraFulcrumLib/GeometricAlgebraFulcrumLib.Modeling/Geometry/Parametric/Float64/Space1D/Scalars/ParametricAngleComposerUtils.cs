using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

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