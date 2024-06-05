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
//    public static XGaConformalParametricElement<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            vectorCurve.ParameterRange,
//            t => conformalSpace.DefineDirectionLine(
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D vectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineDirectionLine(
//                vectorCurve.GetPoint(t).ToXGaVector(conformalSpace.Processor)
//            )
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            bivectorCurve.ParameterRange,
//            t => conformalSpace.DefineDirectionPlane(
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricBivector3D bivectorCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineDirectionPlane(
//                bivectorCurve.GetBivector(t)
//            )
//        );
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlaneFromNormal<T>(this XGaConformalSpace<T> conformalSpace, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            normalCurve.ParameterRange,
//            t => conformalSpace.DefineDirectionPlane(
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> DefineDirectionPlaneFromNormal<T>(this XGaConformalSpace<T> conformalSpace, ScalarRange<T> parameterRange, IParametricCurve3D normalCurve)
//    {
//        return XGaConformalParametricElement<T>.Create(
//            conformalSpace,
//            parameterRange,
//            t => conformalSpace.DefineDirectionPlane(
//                normalCurve.GetPoint(t).NormalToUnitDirection3D()
//            )
//        );
//    }


//}