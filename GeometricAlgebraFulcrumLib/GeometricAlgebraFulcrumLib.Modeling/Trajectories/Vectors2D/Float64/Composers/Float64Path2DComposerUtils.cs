using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Quaternions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Composers;

public static class Float64Path2DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this Float64ScalarSignal curve, Func<double, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this LinFloat64PolarAngleTimeSignal curve, Func<LinFloat64Angle, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this Float64Path2D curve, Func<LinFloat64Vector2D, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this Float64Path3D curve, Func<LinFloat64Vector3D, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this IParametricCurve4D curve, Func<LinFloat64Vector4D, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D ToParametricCurve2D(this IParametricQuaternion curve, Func<LinFloat64Quaternion, LinFloat64Vector2D> vectorMapping)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegmentPath2D CreateLine2D(this ILinFloat64Vector2D point, ILinFloat64Vector2D vector)
    {
        return Float64LineSegmentPath2D.Create(false, point, vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier0Path2D FiniteBezier2D(ILinFloat64Vector2D point1)
    {
        return new Float64Bezier0Path2D(
            false,
            point1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier1Path2D FiniteBezier2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        return new Float64Bezier1Path2D(
            false,
            point1,
            point2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier2Path2D FiniteBezier2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, ILinFloat64Vector2D point3)
    {
        return new Float64Bezier2Path2D(
            false,
            point1,
            point2,
            point3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier3Path2D FiniteBezier2D(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2, ILinFloat64Vector2D point3, ILinFloat64Vector2D point4)
    {
        return new Float64Bezier3Path2D(
            false,
            point1,
            point2,
            point3,
            point4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64CatmullRomSplinePath2D FiniteCatmullRomSpline2D(this IEnumerable<ILinFloat64Vector2D> pointList, CatmullRomSplineType curveType, bool isClosed)
    {
        return new Float64CatmullRomSplinePath2D(false, pointList, curveType, isClosed);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64CirclePath2D CreateCircle2D(this ILinFloat64Vector2D center, double radius)
    {
        return new Float64CirclePath2D(center, radius);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D CreateMathCurve2D(Func<double, double> mathFunction, Float64ScalarRange xRange)
    {
        return Float64ComputedPath2D.Finite(xRange, x => LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)mathFunction(x)));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ArcLengthPath2D ToArcLengthPointPath2D(this Float64Path2D curve)
    {
        return curve as Float64ArcLengthPath2D 
               ?? Float64AdaptiveArcLengthPath2D.Create(curve);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ArcLengthPath2D ToArcLengthPointPath2D(this Float64Path2D curve, Float64AdaptivePath2DSamplingOptions options)
    {
        return curve as Float64ArcLengthPath2D 
               ?? Float64AdaptiveArcLengthPath2D.Create(curve, options);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D FiniteAdaptiveCurve2D(this Float64Path2D curve, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Finite(curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D FiniteAdaptiveCurve2D(this Float64Path2D curve, Float64ScalarRange timeRange, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Finite(timeRange, curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D PeriodicAdaptiveCurve2D(this Float64Path2D curve, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Periodic(curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D PeriodicAdaptiveCurve2D(this Float64Path2D curve, Float64ScalarRange timeRange, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Periodic(timeRange, curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D CreateAdaptiveCurve2D(this Float64Path2D curve, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Create(curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D CreateAdaptiveCurve2D(this Float64Path2D curve, Float64ScalarRange timeRange, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Create(timeRange, curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D CreateAdaptiveCurve2D(this Float64Path2D curve, bool isPeriodic, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Create(isPeriodic, curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath2D CreateAdaptiveCurve2D(this Float64Path2D curve, Float64ScalarRange timeRange, bool isPeriodic, Float64AdaptivePath2DSamplingOptions options)
    {
        return Float64AdaptivePath2D
            .Create(timeRange, isPeriodic, curve)
            .GenerateTree(options);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedPath2D CreateMappedCurve2D(this Float64Path2D surface, IFloat64AffineMap2D map)
    {
        return Float64AffineMappedPath2D.Create(surface, map);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal GetDistanceCurve(this Float64Path2D curve1, ILinFloat64Vector2D curve2)
    {
        return Float64ScalarComputedSignal.Finite(
            curve1.TimeRange,
            t => curve1.GetValue(t).GetDistanceToPoint(curve2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal GetDistanceCurve(this Float64Path2D curve1, Float64Path2D curve2)
    {
        return Float64ScalarComputedSignal.Finite(
            curve1.TimeRange,
            t => curve1.GetValue(t).GetDistanceToPoint(curve2.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetOffsetCurve(this Float64Path2D curve, double offsetVectorX, double offsetVectorY)
    {
        var offsetVector = LinFloat64Vector2D.Create(
            offsetVectorX,
            offsetVectorY
        );

        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVector,
            curve.GetDerivative1Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetOffsetCurve(this Float64Path2D curve, ILinFloat64Vector2D offsetVector)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVector,
            curve.GetDerivative1Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetOffsetCurve(this Float64Path2D curve, Float64Path2D offsetVectorCurve)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVectorCurve.GetValue(t),
            t => curve.GetDerivative1Value(t) + offsetVectorCurve.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetTangentCurve(this Float64Path2D curve)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            curve.GetDerivative1Value,
            curve.GetDerivative2Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetLineNormalCurve(this Float64Path2D curve1, Float64Path2D curve2)
    {
        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t =>
            {
                var p1 = curve1.GetValue(t);
                var p2 = curve2.GetValue(t);

                return (p2 - p1).GetUnitNormal();
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetMidPointCurve(this Float64Path2D curve1, ILinFloat64Vector2D curve2)
    {
        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t => 0.5 * (curve1.GetValue(t) + curve2),
            t => 0.5 * curve1.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetMidPointCurve(this Float64Path2D curve1, Float64Path2D curve2)
    {
        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t => 0.5 * (curve1.GetValue(t) + curve2.GetValue(t)),
            t => 0.5 * (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetMedianPointCurve(this Float64Path2D curve1, Float64Path2D curve2, ILinFloat64Vector2D curve3)
    {
        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + curve2.GetValue(t) + curve3) / 3d,
            t => (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t)) / 3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetMedianPointCurve(this Float64Path2D curve1, ILinFloat64Vector2D curve2, ILinFloat64Vector2D curve3)
    {
        var point = curve2.VectorAdd(curve3);

        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + point) / 3d,
            t => curve1.GetDerivative1Value(t) / 3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D GetMedianPointCurve(this Float64Path2D curve1, Float64Path2D curve2, Float64Path2D curve3)
    {
        return Float64ComputedPath2D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + curve2.GetValue(t) + curve3.GetValue(t)) / 3d,
            t => (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t) + curve3.GetDerivative1Value(t)) / 3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D GetNormalCurve(this Float64Path2D curve)
    {
        return Float64ComputedPath2D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t).DirectionToNormal2D()
        );
    }
}