using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis
{
    public static class GaProductCobNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageVector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageBivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageKVector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaStorageVector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaStorageBivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaStorageKVector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaStorageMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }


    }
}