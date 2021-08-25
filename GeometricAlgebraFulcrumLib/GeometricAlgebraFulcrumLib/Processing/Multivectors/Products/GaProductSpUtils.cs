using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> =>
                    processor.ESp(mv1),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Sp(ortProcessor, mv1),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> =>
                    processor.ESp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Sp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}