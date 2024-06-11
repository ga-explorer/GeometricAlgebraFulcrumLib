//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public static class XGaConformalParametricDirectionComposerUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionLine<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            vectorCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineDirectionLine(
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionLine<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineDirectionLine(
//                vectorCurve.GetPoint(t).ToXGaVector(cgaGeometricSpace.Processor)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            bivectorCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineDirectionPlane(
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineDirectionPlane(
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            normalCurve.ParameterRange,
//            t => cgaGeometricSpace.DefineDirectionPlane(
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlaneFromNormal<T>(this XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            cgaGeometricSpace,
//            parameterRange,
//            t => cgaGeometricSpace.DefineDirectionPlane(
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }


//}