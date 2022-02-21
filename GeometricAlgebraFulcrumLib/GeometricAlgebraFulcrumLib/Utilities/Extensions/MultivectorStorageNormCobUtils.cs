using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageNormCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, VectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, BivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, KVectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this VectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this BivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this KVectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);

            return processor.NormSquared(processor.BasisSet, s1);
        }


    }
}