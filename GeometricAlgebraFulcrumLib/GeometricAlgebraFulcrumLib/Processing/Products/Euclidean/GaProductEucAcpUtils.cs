using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EAcp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.BilinearProduct(mv2, GaBasisUtils.EAcpSignature);
        }
    }
}