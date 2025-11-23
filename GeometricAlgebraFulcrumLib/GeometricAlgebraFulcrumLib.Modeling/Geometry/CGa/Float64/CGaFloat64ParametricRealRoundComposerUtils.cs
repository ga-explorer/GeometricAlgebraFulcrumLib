using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPairFromPoints(Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => RealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPairFromPoints(Float64ScalarRange timeRange, Float64Path2D point1Curve, Float64Path2D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPairFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => RealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPairFromPoints(Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPair(double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPair(double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPair(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPair(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundPointPair(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(double radius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, double radius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphereFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => RealRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphereFromPoints(Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphere(double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphere(Float64ScalarRange timeRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphere(Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RealRoundSphere(Float64ScalarRange timeRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }


}