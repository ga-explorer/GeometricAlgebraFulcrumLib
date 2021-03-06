using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageCpCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Cp<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            var s = processor.Cp(processor.BasisSet, s1, s2);

            return processor.OmOrthonormalToTarget.Map(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Cp<T>(this IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.Map(mv1);
            var s2 = processor.OmTargetToOrthonormal.Map(mv2);

            var s = processor.Cp(processor.BasisSet, s1, s2);

            return processor.OmOrthonormalToTarget.Map(s);
        }

    }
}