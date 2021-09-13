using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageFdpCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Fdp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Fdp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Fdp<T>(this IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Fdp(processor.Signature, s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }

    }
}