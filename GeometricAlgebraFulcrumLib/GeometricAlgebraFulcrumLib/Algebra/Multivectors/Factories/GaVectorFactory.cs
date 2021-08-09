using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories
{
    public static class GaVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGenericVector<T>(this IGaProcessor<T> processor, IGaStorageVector<T> storage)
        {
            return new GaVector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGenericVector<T>(this IGaStorageVector<T> storage, IGaProcessor<T> processor)
        {
            return new GaVector<T>(processor, storage);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGenericVector<T>(this IGaProcessor<T> processor, int index)
        {
            return new GaVector<T>(
                processor, 
                processor.CreateStorageBasisVector(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGenericVector<T>(this IGaProcessor<T> processor, ulong index)
        {
            return new GaVector<T>(
                processor, 
                processor.CreateStorageBasisVector(index)
            );
        }
    }
}