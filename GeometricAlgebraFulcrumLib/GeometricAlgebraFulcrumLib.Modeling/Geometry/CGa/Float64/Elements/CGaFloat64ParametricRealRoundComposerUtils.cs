using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricRealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double radius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }


}