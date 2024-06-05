//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricFlatComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPoint<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D positionCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatPoint(
//                positionCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPoint<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatPoint(
//                positionCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IFloat64ParametricCurve2D point1Curve, IFloat64ParametricCurve2D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineFlatLineFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineFlatLineFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatLineFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLine<T>(this XGaConformalSpace<T> conformalSpace, IFloat64ParametricCurve2D positionCurve, IFloat64ParametricCurve2D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatLine(
//                positionCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLine<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatLine(
//                positionCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatLine<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatLine(
//                positionCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            point1Curve.ParameterRange,
//            t => conformalSpace.DefineFlatPlaneFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatPlaneFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D positionCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                positionCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                positionCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            positionCurve.ParameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineFlatPlane<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineFlatPlane(
//                positionCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//}