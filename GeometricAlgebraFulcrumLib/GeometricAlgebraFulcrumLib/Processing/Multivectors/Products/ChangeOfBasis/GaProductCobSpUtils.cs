using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis
{
    public static class GaProductCobSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Sp(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessorChangeOfBasis<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            return processor.Sp(processor.Signature, s1, s2);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaStorageMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Sp(processor.Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            return processor.Sp(processor.Signature, s1, s2);
        }
    }
}