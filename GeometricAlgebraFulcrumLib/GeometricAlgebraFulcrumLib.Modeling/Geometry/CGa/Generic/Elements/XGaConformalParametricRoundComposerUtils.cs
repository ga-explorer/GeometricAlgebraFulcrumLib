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
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve, IFloat64ParametricCurve2D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircleFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircleFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundCircle(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundSphere(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundSphere(
//                squaredRadius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRoundSphere(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRoundSphere(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }
    
//}