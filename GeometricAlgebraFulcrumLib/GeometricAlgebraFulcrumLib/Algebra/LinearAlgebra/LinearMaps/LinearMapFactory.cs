using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps
{
    public static class LinearMapFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixStorage<T> matrix)
        {
            return new LinUnilinearMap<T>(linearProcessor, matrix);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaSparseUnilinearMap<T>(geometricProcessor);
        }

        public static GaSparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Func<ulong, IMultivectorStorage<T>> mappingFunc)
        {
            var linearMap = new GaSparseUnilinearMap<T>(geometricProcessor);

            var gaSpaceDimension = geometricProcessor.GaSpaceDimension;

            for (var id = 0UL; id < gaSpaceDimension; id++)
                linearMap[id] = mappingFunc(id);

            return linearMap;
        }
    }
}