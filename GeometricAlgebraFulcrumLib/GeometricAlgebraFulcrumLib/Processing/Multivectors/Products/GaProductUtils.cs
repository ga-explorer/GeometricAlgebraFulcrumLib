using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Dual<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Dual<T>(this IGaStorageMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> UnDual<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> UnDual<T>(this IGaStorageMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> BladeInverse<T>(this IGaProcessor<T> processor, IGaStorageKVector<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> BladeInverse<T>(this IGaStorageKVector<T> mv1, IGaProcessor<T> processor)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> BladeInverse<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return processor.Divide(mv1, bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> VersorInverse<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> VersorInverse<T>(this IGaStorageMultivector<T> mv1, IGaProcessor<T> processor)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return processor.Divide(processor.Reverse(mv1), versorSpReverse);
        }
    }
}