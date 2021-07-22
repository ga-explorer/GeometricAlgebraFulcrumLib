using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis
{
    public static class GaProductCobSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Signature.Sp(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            return processor.Signature.Sp(s1, s2);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGasMultivector<T> mv1, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);

            return processor.Signature.Sp(s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            return processor.Signature.Sp(s1, s2);
        }
    }
}