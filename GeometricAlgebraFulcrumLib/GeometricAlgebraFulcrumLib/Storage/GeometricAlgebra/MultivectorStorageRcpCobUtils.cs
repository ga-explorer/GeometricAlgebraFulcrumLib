using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageRcpCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Rcp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);
            var s2 = processor.OmTargetToOrthonormal.OmMap(mv2);

            var s = processor.Rcp(processor.BasisSet, s1, s2);

            return processor.OmOrthonormalToTarget.OmMap(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Rcp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            var s = processor.Rcp(processor.BasisSet, s1, s2);

            return processor.OmOrthonormalToTarget.Map(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Rcp<T>(this IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            var s = processor.Rcp(processor.BasisSet, s1, s2);

            return processor.OmOrthonormalToTarget.Map(s);
        }
    }
}