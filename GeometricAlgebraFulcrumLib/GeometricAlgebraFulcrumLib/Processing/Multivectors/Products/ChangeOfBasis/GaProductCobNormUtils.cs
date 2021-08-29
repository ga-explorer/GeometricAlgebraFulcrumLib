using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis
{
    public static class GaProductCobNormUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaVectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaBivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaKVectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessorChangeOfBasis<T> processor, IGaMultivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaVectorStorage<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaBivectorStorage<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapBivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaKVectorStorage<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapKVector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaMultivectorStorage<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.NormSquared(processor.Signature, s1);
        }


    }
}