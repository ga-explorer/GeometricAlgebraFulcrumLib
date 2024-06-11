//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricImaginaryRoundComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPairFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPairFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Scalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
//                squaredRadius,
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundPointPair<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> squaredRadius, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundPointPair(
//                squaredRadius.GetValue(t),
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                centerCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius,
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundCircle<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundCircle(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphereFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphereFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve, IParametricCurve3D point4Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphereFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t),
//                point4Curve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
//                radius,
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineImaginaryRoundSphere<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricScalar<T> radius, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineImaginaryRoundSphere(
//                radius.GetValue(t),
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//}