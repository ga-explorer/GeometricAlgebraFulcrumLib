using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageCpEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> ECp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, BasisBladeProductUtils.ECpSign);
        }

    }
}