using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageCpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Cp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ECp(mv1, mv2),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Cp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Cp(mv1, mv2)
            };
        }
    }
}