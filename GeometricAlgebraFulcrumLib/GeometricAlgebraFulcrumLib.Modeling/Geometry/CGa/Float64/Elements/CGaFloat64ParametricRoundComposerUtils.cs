using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ParametricRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPoint(
                centerCurve.GetPoint(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius,
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundPointPair(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t).ToRGaFloat64Vector(),
                vectorCurve.GetPoint(t).ToRGaFloat64Vector()
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IFloat64ParametricCurve2D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            point1Curve.ParameterRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
                point1Curve.GetPoint(t),
                point2Curve.GetPoint(t),
                point3Curve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                normalCurve.GetPoint(t).NormalToUnitDirection3D()
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius,
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundCircle(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t),
                bivectorCurve.GetBivector(t)
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, double squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius,
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            centerCurve.ParameterRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, IFloat64ParametricScalar squaredRadius, IParametricCurve3D centerCurve)
    {
        return CGaFloat64ParametricElement.Create(
            cgaGeometricSpace,
            parameterRange,
            t => cgaGeometricSpace.DefineRoundSphere(
                squaredRadius.GetValue(t),
                centerCurve.GetPoint(t)
            )
        );
    }

}