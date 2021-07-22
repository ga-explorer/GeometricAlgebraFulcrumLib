using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Multivectors
{
    public static class GaMultivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGenericMultivector<T>(this IGaProcessor<T> processor, IGasMultivector<T> storage)
        {
            return new GaMultivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ToGenericMultivector<T>(this IGasMultivector<T> storage, IGaProcessor<T> processor)
        {
            return new GaMultivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ToGenericMultivector<T>(this IGasMultivector<T> storage)
        {
            var processor = storage.ScalarProcessor as IGaProcessor<T>;

            return new GaMultivector<T>(processor, storage);
        }
    }
}
