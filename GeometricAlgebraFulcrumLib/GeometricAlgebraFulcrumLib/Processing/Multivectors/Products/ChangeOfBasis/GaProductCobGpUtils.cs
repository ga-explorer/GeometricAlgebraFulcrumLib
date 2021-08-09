using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis
{
    public static class GaProductCobGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Gp(processor.Signature, s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Gp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaStorageMultivector<T> mv3)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);
            var s3 = processor.OmTargetToOrthonormal.MapMultivector(mv3);

            var s = processor.Gp(processor.Signature, s1, s2, s3);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.GpReverse(processor.Signature, s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.GpReverse(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaStorageMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Gp(processor.Signature, s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Gp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaStorageMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.GpReverse(processor.Signature, s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.GpReverse(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
    }
}
