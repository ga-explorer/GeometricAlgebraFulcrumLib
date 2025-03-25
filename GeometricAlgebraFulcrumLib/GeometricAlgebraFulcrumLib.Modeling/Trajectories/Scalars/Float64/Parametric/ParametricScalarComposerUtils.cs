using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Quaternions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Parametric;

public static class ParametricScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this Float64ScalarSignal curve, Func<double, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this LinFloat64PolarAngleTimeSignal curve, Func<LinFloat64Angle, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this Float64Path2D curve, Func<LinFloat64Vector2D, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this Float64Path3D curve, Func<LinFloat64Vector3D, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this IParametricCurve4D curve, Func<LinFloat64Vector4D, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal ToParametricScalar(this IParametricQuaternion curve, Func<LinFloat64Quaternion, double> vectorMapping)
    {
        return Float64ScalarComputedSignal.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this Float64ScalarSignal baseSignal, double t)
    {
        return baseSignal.GetValue(t).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetDerivative1Angle(this Float64ScalarSignal baseSignal, double t)
    {
        return baseSignal.GetDerivative1Value(t).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetDerivative2Angle(this Float64ScalarSignal baseSignal, double t)
    {
        return baseSignal.GetDerivative2Value(t).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this Float64ScalarSignal curve)
    {
        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(curve);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this Float64ScalarSignal curve, Func<double, LinFloat64PolarAngle> vectorMapping)
    {
        return LinFloat64PolarAngleTimeSignal.Create(curve, vectorMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D CreatePolarCurve(this Float64ScalarSignal angle, double radius, LinFloat64Vector3D center, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2)
    {
        return Float64ComputedPath3D.Finite(
            angle.TimeRange,
            t =>
            {
                var a = angle.GetAngle(t);

                return center + radius * (a.Cos() * direction1 + a.Sin() * direction2);
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D CreatePolarCurve(this LinFloat64PolarAngleTimeSignal angle, double radius, LinFloat64Vector3D center, LinFloat64Vector3D direction1, LinFloat64Vector3D direction2)
    {
        return Float64ComputedPath3D.Finite(
            angle.TimeRange,
            t =>
            {
                var a = angle.GetAngle(t);

                return center + radius * (a.Cos() * direction1 + a.Sin() * direction2);
            }
        );
    }
}