using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageFdpUtils
    {
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