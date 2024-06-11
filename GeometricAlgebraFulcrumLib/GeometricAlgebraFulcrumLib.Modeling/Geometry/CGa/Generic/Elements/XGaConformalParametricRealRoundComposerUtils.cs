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
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IFloat64ParametricCurve2D centerCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IFloat64ParametricCurve2D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineRealRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//}