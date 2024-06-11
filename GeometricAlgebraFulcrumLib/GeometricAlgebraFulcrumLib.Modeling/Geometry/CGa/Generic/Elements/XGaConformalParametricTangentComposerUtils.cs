//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricTangentComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPoint<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentPoint(
//                centerCurve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentLine<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentLine(
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentLine<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentLine(
//                centerCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor),
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentLineFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentLineFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlane<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlane<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                centerCurve.GetPoint(t),
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D positionNormalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            positionNormalCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                positionNormalCurve.GetPoint(t),
//                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D positionNormalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                positionNormalCurve.GetPoint(t),
//                positionNormalCurve.GetDerivative1Point(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            centerCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D centerCurve, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentPlane(
//                centerCurve.GetPoint(t),
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            point1Curve.ParameterRange,
//            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D point1Curve, IParametricCurve3D point2Curve, IParametricCurve3D point3Curve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineTangentPlaneFromPoints(
//                point1Curve.GetPoint(t),
//                point2Curve.GetPoint(t),
//                point3Curve.GetPoint(t)
//            )
//        );
//    }

//}