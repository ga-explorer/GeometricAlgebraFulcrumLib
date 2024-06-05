using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public static class RGaConformalParametricRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPoint(this RGaConformalSpace conformalSpace, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPoint(this RGaConformalSpace conformalSpace, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, double squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundPointPair(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, double squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircleFromPoints(this RGaConformalSpace conformalSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircleFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircleFromPoints(this RGaConformalSpace conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            point1Curve.ParameterRange,
            t => conformalSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircleFromPoints(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundCircle(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundSphere(this RGaConformalSpace conformalSpace, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundSphere(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundSphere(this RGaConformalSpace conformalSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            centerCurve.ParameterRange,
            t => conformalSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement DefineRoundSphere(this RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return RGaConformalParametricElement.Create(
            conformalSpace,
            parameterRange,
            t => conformalSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }
    
}