using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis
{
    public static class GaProductCobNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGasVector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.Signature.NormSquared(s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGasBivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.Signature.NormSquared(s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGasKVector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.Signature.NormSquared(s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Signature.NormSquared(s1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGasVector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.Signature.NormSquared(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGasBivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.Signature.NormSquared(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGasKVector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.Signature.NormSquared(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGasMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Signature.NormSquared(s1);
        }


    }
}