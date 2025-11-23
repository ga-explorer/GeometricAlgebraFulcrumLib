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
    public CGaFloat64ParametricElement RoundPoint(Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPoint(Float64ScalarRange timeRange, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPoint(Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPoint(Float64ScalarRange timeRange, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPoint(
                centerCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarRange timeRange, double squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarSignal squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve, Float64Path2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundPointPair(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(double squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, double squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarSignal squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircleFromPoints(Float64Path2D point1Curve, Float64Path2D point2Curve, Float64Path2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => RealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircleFromPoints(Float64ScalarRange timeRange, Float64Path2D point1Curve, Float64Path2D point2Curve, Float64Path2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircleFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => RealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircleFromPoints(Float64ScalarRange timeRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RealRoundCircleFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(double squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(double squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundCircle(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundSphere(double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundSphere(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundSphere(Float64ScalarRange timeRange, double squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundSphere(
                squaredRadius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundSphere(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => RoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement RoundSphere(Float64ScalarRange timeRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            timeRange,
            t => RoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

}