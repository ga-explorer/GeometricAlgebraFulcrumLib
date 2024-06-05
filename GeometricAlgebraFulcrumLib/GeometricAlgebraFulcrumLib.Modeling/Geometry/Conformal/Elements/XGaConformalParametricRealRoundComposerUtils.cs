//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Parametric.Space1D;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricRealRoundComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineRealRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRealRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//}