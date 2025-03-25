using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToRGaFloat64Vector(),
                vectorCurve.GetValue(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path2D point1Curve, Float64Path2D point2Curve, Float64Path2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path2D point1Curve, Float64Path2D point2Curve, Float64Path2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            timeRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

}