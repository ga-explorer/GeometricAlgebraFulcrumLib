//using System;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
//using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps
//{
//    public static class LinearMapFactory
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static GaSparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
//        {
//            return new GaSparseUnilinearMap<T>(geometricProcessor);
//        }

//        public static GaSparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Func<ulong, IMultivectorStorage<T>> mappingFunc)
//        {
//            var linearMap = new GaSparseUnilinearMap<T>(geometricProcessor);

//            var gaSpaceDimensions = geometricProcessor.GaSpaceDimensions;

//            for (var id = 0UL; id < gaSpaceDimensions; id++)
//                linearMap[id] = mappingFunc(id);

//            return linearMap;
//        }
//    }
//}