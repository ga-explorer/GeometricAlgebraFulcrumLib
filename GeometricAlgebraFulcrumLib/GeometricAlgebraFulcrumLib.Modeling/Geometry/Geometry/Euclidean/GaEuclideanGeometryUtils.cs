//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

//namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
//{
//    public static class GaEuclideanGeometryUtils
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static T GetEuclideanAngle<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> v1, VectorStorage<T> v2, bool assumeUnitVectors = false)
//        {
//            if (assumeUnitVectors)
//                return scalarProcessor.ArcCos(
//                    scalarProcessor.ESp(v1, v2)
//                );

//            return scalarProcessor.ArcCos(
//                scalarProcessor.Divide(
//                    scalarProcessor.ESp(v1, v2),
//                    scalarProcessor.Sqrt(
//                        scalarProcessor.Times(
//                            scalarProcessor.ENormSquared(v1),
//                            scalarProcessor.ENormSquared(v2)
//                        )
//                    )
//                )
//            );
//        }
        
        
        
//    }
//}
