using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageSpCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);

            return processor.Sp(processor.BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            return processor.Sp(processor.BasisSet, s1, s2);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);

            return processor.Sp(processor.BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            return processor.Sp(processor.BasisSet, s1, s2);
        }
    }
}