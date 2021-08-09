using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.EGp(mv1),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Gp(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.EGp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Gp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaStorageMultivector<T> mv3)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.EGp(mv1, mv2, mv3),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Gp(ortProcessor, mv1, mv2, mv3),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2, mv3)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.EGpReverse(mv1),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.GpReverse(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.EGpReverse(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.GpReverse(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}