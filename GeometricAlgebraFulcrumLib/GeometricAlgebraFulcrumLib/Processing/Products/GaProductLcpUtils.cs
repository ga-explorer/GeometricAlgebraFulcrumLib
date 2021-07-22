using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Lcp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ELcp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtLcpUtils.Lcp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobLcpUtils.Lcp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Lcp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ELcp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtLcpUtils.Lcp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobLcpUtils.Lcp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}