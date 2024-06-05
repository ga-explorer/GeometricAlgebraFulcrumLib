//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Parametric.Space1D;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricRoundComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundSphere(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundSphere(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            centerCurve.ParameterRange,
//            t => conformalSpace.DefineRoundSphere(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineRoundSphere(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    
//}