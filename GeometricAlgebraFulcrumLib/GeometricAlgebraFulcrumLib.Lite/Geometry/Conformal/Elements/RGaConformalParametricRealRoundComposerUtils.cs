using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public static class RGaConformalParametricRealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve2D point1Curve, IParametricCurve2D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve2D point1Curve, IParametricCurve2D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundPointPairFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t)
            )
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve2D centerCurve, IParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, IParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, IParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, IParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, IParametricScalar radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricScalar radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).UnDual3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, IParametricScalar radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricScalar radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircle(
                radius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineRealRoundSphereFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t),
                point4Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundSphereFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t),
                point4Curve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphere(this RGaConformalSpace conformalSpace, double radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundSphere(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphere(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundSphere(
                radius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphere(this RGaConformalSpace conformalSpace, IParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRealRoundSphere(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricScalar radius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundSphere(
                radius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }


}