using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.ScalarAlgebra
{
    public static class ScalarStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new ScalarComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return new ScalarComposer<T>(scalarProcessor, scalar);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> SumToStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalars)
        //{
        //    return KVectorStorage<T>.CreateScalar(
        //        scalarProcessor.Add(scalars)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> SumToStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T scalar2)
        //{
        //    return KVectorStorage<T>.CreateScalar(
        //        scalarProcessor.Add(scalar1, scalar2)
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> SumToStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalars)
        //{
        //    return KVectorStorage<T>.CreateScalar(
        //        scalarProcessor.Add(scalars)
        //    );
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateStorageZeroScalar<T>()
        //{
        //    return KVectorStorage<T>.ZeroScalar;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateStorageZeroScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        //{
        //    return KVectorStorage<T>.ZeroScalar;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateKVectorBasisScalarStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        //{
        //    return KVectorStorage<T>.CreateScalar(
        //        scalarProcessor.ScalarOne
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateStorageBasisScalarNegative<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        //{
        //    return KVectorStorage<T>.CreateScalar(
        //        scalarProcessor.ScalarMinusOne
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateKVectorScalarStorage<T>(this T scalar)
        //{
        //    return KVectorStorage<T>.CreateScalar(scalar);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static GeoScalarStorage<T> CreateKVectorScalarStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        //{
        //    return KVectorStorage<T>.CreateScalar(scalar);
        //}
    }
}