using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;

public partial class CGaFloat64GeometricSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPairFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => ImaginaryRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPairFromPoints(Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPair(double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPair(Float64ScalarRange parameterRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPair(Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundPointPair(Float64ScalarRange parameterRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaVector(),
                vectorCurve.GetValue(t).ToXGaVector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundCircle(Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphereFromPoints(Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            point1Curve.TimeRange,
            t => ImaginaryRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphereFromPoints(Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphere(double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphere(Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphere(Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            centerCurve.TimeRange,
            t => ImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64ParametricElement ImaginaryRoundSphere(Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            this,
            parameterRange,
            t => ImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }


}