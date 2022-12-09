using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Rcp<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ERcp(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Rcp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Rcp(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Rcp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ERcp(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Rcp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Rcp(mv1, mv2)
            };
        }
    }
}