using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Dual<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Dual<T>(this IGaMultivectorStorage<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> UnDual<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> UnDual<T>(this IGaMultivectorStorage<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> BladeInverse<T>(this IGaProcessor<T> processor, IGaKVectorStorage<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> BladeInverse<T>(this IGaKVectorStorage<T> mv1, IGaProcessor<T> processor)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> BladeInverse<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> VersorInverse<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> VersorInverse<T>(this IGaMultivectorStorage<T> mv1, IGaProcessor<T> processor)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }
    }
}