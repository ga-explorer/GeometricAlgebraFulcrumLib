using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Dual<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Dual<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarInverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> UnDual<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> UnDual<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor.Lcp(mv1, processor.PseudoScalarReverse);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> BladeInverse<T>(this IGaProcessor<T> processor, IGasKVector<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> BladeInverse<T>(this IGasKVector<T> mv1, IGaProcessor<T> processor)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> BladeInverse<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            var bladeSpSquared = processor.Sp(mv1);

            return mv1.Divide(bladeSpSquared);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> VersorInverse<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> VersorInverse<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            var versorSpReverse = processor.NormSquared(mv1);

            return mv1.GetReverse().Divide(versorSpReverse);
        }
    }
}