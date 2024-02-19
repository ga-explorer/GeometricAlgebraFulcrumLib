using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

internal static class MultivectorStorageAcpEucUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> EAcp<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
    {
        return scalarProcessor.BilinearProduct(mv1, mv2, BasisBladeProductUtils.EAcpSign);
    }
}