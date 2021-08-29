using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaMultivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGenericMultivector<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> storage)
        {
            return new GaMultivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGenericMultivector<T>(this IGaMultivectorStorage<T> storage, IGaProcessor<T> processor)
        {
            return new GaMultivector<T>(processor, storage);
        }
    }
}
