using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Quaternions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;

public static class AngleTimeSignalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this LinFloat64PolarAngleTimeSignal curve, Func<LinFloat64PolarAngle, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
                curve.TimeRange,
                t => vectorMapping(curve.GetAngle(t))
            );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this Float64Path2D curve, Func<LinFloat64Vector2D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this Float64Path3D curve, Func<LinFloat64Vector3D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this IParametricCurve4D curve, Func<LinFloat64Vector4D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetPoint(t))
        );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal ToPolarAngle(this IParametricQuaternion curve, Func<LinFloat64Quaternion, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );

        return LinFloat64PolarAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this LinFloat64DirectedAngleTimeSignal curve, Func<LinFloat64DirectedAngle, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetAngle(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this Float64Path2D curve, Func<LinFloat64Vector2D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this Float64Path3D curve, Func<LinFloat64Vector3D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this IParametricCurve4D curve, Func<LinFloat64Vector4D, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetPoint(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal ToDirectedAngle(this IParametricQuaternion curve, Func<LinFloat64Quaternion, double> vectorMapping)
    {
        var baseSignal = 
            Float64ScalarSignal.FiniteComputed(
            curve.TimeRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );

        return LinFloat64DirectedAngleTimeSignal.CreateFromRadians(baseSignal);
    }
}