using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space4D.Curves.CatmullRom;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Quaternions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Bezier;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;

public static class Float64Path3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this Float64ScalarSignal curve, Func<double, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this LinFloat64PolarAngleTimeSignal curve, Func<LinFloat64Angle, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetAngle(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this Float64Path2D curve, Func<LinFloat64Vector2D, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToXyParametricCurve3D(this Float64Path2D curve)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t).ToXyLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this Float64Path3D curve, Func<LinFloat64Vector3D, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this IParametricBivector2D curve, Func<LinFloat64Bivector2D, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this IParametricBivector3D curve, Func<LinFloat64Bivector3D, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this IParametricCurve4D curve, Func<LinFloat64Vector4D, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetPoint(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D ToParametricCurve3D(this IParametricQuaternion curve, Func<LinFloat64Quaternion, LinFloat64Vector3D> vectorMapping)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => vectorMapping(curve.GetQuaternion(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D XyDirectionToNormal3D(this IParametricBivector2D curve, LinFloat64Vector3D? zeroNormal = null)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t).ToXyBivector3D().DirectionToNormal3D(zeroNormal)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D XyNormalToDirection3D(this IParametricBivector2D curve, LinFloat64Vector3D? zeroNormal = null)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t).ToXyBivector3D().NormalToDirection3D(zeroNormal)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier0Path3D CreateBezier3D(ILinFloat64Vector3D point1)
    {
        return new Float64Bezier0Path3D(
            false,
            point1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier1Path3D CreateBezier3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        return new Float64Bezier1Path3D(
            false,
            point1,
            point2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier2Path3D CreateBezier3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3)
    {
        return new Float64Bezier2Path3D(
            false,
            point1,
            point2,
            point3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bezier3Path3D CreateBezier3D(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3, ILinFloat64Vector3D point4)
    {
        return new Float64Bezier3Path3D(
            false,
            point1,
            point2,
            point3,
            point4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64CatmullRomSplinePath3D CreateCatmullRomSpline3D(this IEnumerable<ILinFloat64Vector3D> pointList, CatmullRomSplineType curveType, bool isClosed)
    {
        return new Float64CatmullRomSplinePath3D(false, pointList, curveType, isClosed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CatmullRomSpline4D CreateCatmullRomSpline4D(this IEnumerable<ILinFloat64Vector4D> pointList, CatmullRomSplineType curveType, bool isClosed)
    {
        return new CatmullRomSpline4D(pointList, curveType, isClosed);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AxisAlignedCirclePath3D CreateCircle3D(this LinBasisVector3D normalAxis, double radius, int rotationCount = 1)
    {
        return normalAxis switch
        {
            LinBasisVector3D.Px => new Float64YzCirclePath3D(radius, rotationCount),
            LinBasisVector3D.Nx => new Float64YzCirclePath3D(radius, -rotationCount),
            LinBasisVector3D.Py => new Float64ZxCirclePath3D(radius, rotationCount),
            LinBasisVector3D.Ny => new Float64ZxCirclePath3D(radius, -rotationCount),
            LinBasisVector3D.Pz => new Float64XyCirclePath3D(radius, rotationCount),
            _ => new Float64XyCirclePath3D(radius, -rotationCount)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64CirclePath3D CreateCircle3D(this ILinFloat64Vector3D unitNormal, double radius, int rotationCount = 1)
    {
        return new Float64CirclePath3D(LinFloat64Vector3D.Zero, unitNormal, radius, rotationCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64CirclePath3D CreateCircle3D(this ILinFloat64Vector3D unitNormal, ILinFloat64Vector3D center, double radius, int rotationCount = 1)
    {
        return new Float64CirclePath3D(center, unitNormal, radius, rotationCount);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath2D CreateMathCurve2D(Float64ScalarRange xRange, Func<double, double> mathFunction)
    {
        return Float64ComputedPath2D.Finite(xRange, x => LinFloat64Vector2D.Create((Float64Scalar)x, (Float64Scalar)mathFunction(x)));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ArcLengthPath3D ToArcLengthPointPath3D(this Float64Path3D curve)
    {
        return curve as Float64ArcLengthPath3D 
               ?? Float64AdaptiveArcLengthPath3D.Create(curve);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ArcLengthPath3D ToArcLengthPointPath3D(this Float64Path3D curve, Float64AdaptivePath3DSamplingOptions options)
    {
        return curve as Float64ArcLengthPath3D 
               ?? Float64AdaptiveArcLengthPath3D.Create(curve, options);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D FiniteAdaptiveCurve3D(this Float64Path3D curve, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Finite(curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D FiniteAdaptiveCurve3D(this Float64Path3D curve, Float64ScalarRange timeRange, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Finite(timeRange, curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D PeriodicAdaptiveCurve3D(this Float64Path3D curve, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Periodic(curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D PeriodicAdaptiveCurve3D(this Float64Path3D curve, Float64ScalarRange timeRange, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Periodic(timeRange, curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D CreateAdaptiveCurve3D(this Float64Path3D curve, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Create(curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D CreateAdaptiveCurve3D(this Float64Path3D curve, Float64ScalarRange timeRange, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Create(timeRange, curve)
            .GenerateTree(options);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D CreateAdaptiveCurve3D(this Float64Path3D curve, bool isPeriodic, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Create(isPeriodic, curve)
            .GenerateTree(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptivePath3D CreateAdaptiveCurve3D(this Float64Path3D curve, Float64ScalarRange timeRange, bool isPeriodic, Float64AdaptivePath3DSamplingOptions options)
    {
        return Float64AdaptivePath3D
            .Create(timeRange, isPeriodic, curve)
            .GenerateTree(options);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedPath3D CreateMappedCurve3D(this Float64Path3D curve, IFloat64AffineMap3D map)
    {
        return Float64AffineMappedPath3D.Create(curve, map);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComputedSignal GetDistanceCurve(this Float64Path3D curve1, ILinFloat64Vector3D curve2)
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
    public static Float64ScalarComputedSignal GetDistanceCurve(this Float64Path3D curve1, Float64Path3D curve2)
    {
        return Float64ScalarComputedSignal.Finite(
            curve1.TimeRange,
            t => curve1.GetValue(t).GetDistanceToPoint(curve2.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetOffsetCurve(this Float64Path3D curve, double offsetVectorX, double offsetVectorY, double offsetVectorZ)
    {
        var offsetVector = LinFloat64Vector3D.Create(
            offsetVectorX,
            offsetVectorY,
            offsetVectorZ
        );

        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVector,
            curve.GetDerivative1Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetOffsetCurve(this Float64Path3D curve, ILinFloat64Vector3D offsetVector)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVector,
            curve.GetDerivative1Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetOffsetCurve(this Float64Path3D curve, Float64Path3D offsetVectorCurve)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            t => curve.GetValue(t) + offsetVectorCurve.GetValue(t),
            t => curve.GetDerivative1Value(t) + offsetVectorCurve.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetTangentCurve(this Float64Path3D curve)
    {
        return Float64ComputedPath3D.Finite(
            curve.TimeRange,
            curve.GetDerivative1Value,
            curve.GetDerivative2Value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetPlaneNormalCurve(this Float64Path3D curve1, Float64Path3D curve2, Float64Path3D curve3)
    {
        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t =>
            {
                var p1 = curve1.GetValue(t);
                var p2 = curve2.GetValue(t);
                var p3 = curve3.GetValue(t);

                return (p2 - p1).VectorUnitCross(p3 - p2);
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetMidPointCurve(this Float64Path3D curve1, ILinFloat64Vector3D curve2)
    {
        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t => 0.5 * (curve1.GetValue(t) + curve2),
            t => 0.5 * curve1.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetMidPointCurve(this Float64Path3D curve1, Float64Path3D curve2)
    {
        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t => 0.5 * (curve1.GetValue(t) + curve2.GetValue(t)),
            t => 0.5 * (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetMedianPointCurve(this Float64Path3D curve1, Float64Path3D curve2, ILinFloat64Vector3D curve3)
    {
        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + curve2.GetValue(t) + curve3) / 3d,
            t => (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t)) / 3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetMedianPointCurve(this Float64Path3D curve1, ILinFloat64Vector3D curve2, ILinFloat64Vector3D curve3)
    {
        var point = curve2.VectorAdd(curve3);

        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + point) / 3d,
            t => curve1.GetDerivative1Value(t) / 3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ComputedPath3D GetMedianPointCurve(this Float64Path3D curve1, Float64Path3D curve2, Float64Path3D curve3)
    {
        return Float64ComputedPath3D.Finite(
            curve1.TimeRange,
            t => (curve1.GetValue(t) + curve2.GetValue(t) + curve3.GetValue(t)) / 3d,
            t => (curve1.GetDerivative1Value(t) + curve2.GetDerivative1Value(t) + curve3.GetDerivative1Value(t)) / 3
        );
    }

}