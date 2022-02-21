using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Acp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EAcp(mv1, mv2),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Acp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Acp(mv1, mv2)
            };
        }
    }
}