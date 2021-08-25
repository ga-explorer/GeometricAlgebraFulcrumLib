using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductFdpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Fdp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> =>
                    processor.EFdp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Fdp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobFdpUtils.Fdp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}