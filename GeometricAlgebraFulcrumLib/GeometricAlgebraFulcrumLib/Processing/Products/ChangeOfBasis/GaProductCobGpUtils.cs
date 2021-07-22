using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis
{
    public static class GaProductCobGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Signature.Gp(s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.Gp(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGasMultivector<T> mv3)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);
            var s3 = processor.OmTargetToOrthonormal.MapMultivector(mv3);

            var s = processor.Signature.Gp(s1, s2, s3);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Signature.GpReverse(s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.GpReverse(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Signature.Gp(s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.Gp(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            var s = processor.Signature.GpReverse(s1);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.GpReverse(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
    }
}
