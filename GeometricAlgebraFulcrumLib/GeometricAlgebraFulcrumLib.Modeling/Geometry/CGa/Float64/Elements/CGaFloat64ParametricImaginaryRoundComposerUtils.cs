using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricImaginaryRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64ScalarSignal squaredRadius, Float64Path3D centerCurve, Float64Path3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetValue(t).ToXGaFloat64Vector(),
                vectorCurve.GetValue(t).ToXGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                centerCurve.GetDerivative1Value(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve, Float64Path3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                normalCurve.GetValue(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetValue(t),
                bivectorCurve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64Path3D point1Curve, Float64Path3D point2Curve, Float64Path3D point3Curve, Float64Path3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
                point1Curve.GetValue(t),
                point2Curve.GetValue(t),
                point3Curve.GetValue(t),
                point4Curve.GetValue(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius,
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.TimeRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Float64ScalarSignal radius, Float64Path3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetValue(t)
            )
        );
    }


}