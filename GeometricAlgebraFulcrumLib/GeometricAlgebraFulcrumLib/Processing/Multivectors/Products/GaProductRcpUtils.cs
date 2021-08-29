using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Rcp<T>(this IGaProcessor<T> processor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> =>
                    processor.ERcp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Rcp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobRcpUtils.Rcp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}