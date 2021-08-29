using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Acp<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> =>
                    processor.EAcp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Acp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobAcpUtils.Acp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}