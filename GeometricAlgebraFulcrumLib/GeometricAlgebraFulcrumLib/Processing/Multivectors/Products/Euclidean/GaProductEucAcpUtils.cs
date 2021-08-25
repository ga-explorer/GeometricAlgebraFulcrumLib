using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EAcp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, GaBasisBladeProductUtils.EAcpSignature);
        }
    }
}