using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricImaginaryRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t),
                point4Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t),
                point4Curve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
                radius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }


}