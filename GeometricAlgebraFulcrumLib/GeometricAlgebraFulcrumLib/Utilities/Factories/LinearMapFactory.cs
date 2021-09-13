using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LinearMapFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this ILinearAlgebraProcessor<T> linearProcessor, ILinMatrixStorage<T> matrix)
        {
            return new LinUnilinearMap<T>(linearProcessor, matrix);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new SparseUnilinearMap<T>(geometricProcessor);
        }

        public static SparseUnilinearMap<T> CreateSparseUnilinearMap<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Func<ulong, IMultivectorStorage<T>> mappingFunc)
        {
            var linearMap = new SparseUnilinearMap<T>(geometricProcessor);

            var gaSpaceDimension = geometricProcessor.GaSpaceDimension;

            for (var id = 0UL; id < gaSpaceDimension; id++)
                linearMap[id] = mappingFunc(id);

            return linearMap;
        }
    }
}