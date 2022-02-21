using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageFdpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Fdp<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EFdp(mv1, mv2),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Fdp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Fdp(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Fdp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EFdp(mv1, mv2),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Fdp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Fdp(mv1, mv2)
            };
        }
    }
}