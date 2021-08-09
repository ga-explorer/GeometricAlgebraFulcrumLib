using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Lcp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.ELcp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Lcp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobLcpUtils.Lcp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}