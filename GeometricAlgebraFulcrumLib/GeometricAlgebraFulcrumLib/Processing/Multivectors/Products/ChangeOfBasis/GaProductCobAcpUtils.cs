using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis
{
    public static class GaProductCobAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Acp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Acp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Acp<T>(this IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Acp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

    }
}