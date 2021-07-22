using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGasMultivector<T> mv3)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(mv2).EGp(mv3),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1, mv2, mv3),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2, mv3)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGpReverse(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.GpReverse(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGpReverse(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.GpReverse(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGasMultivector<T> mv3, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGp(mv2).EGp(mv3),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.Gp(ortProcessor, mv1, mv2, mv3),

                _ =>
                    GaProductCobGpUtils.Gp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2, mv3)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGpReverse(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.GpReverse(ortProcessor, mv1),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EGpReverse(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtGpUtils.GpReverse(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobGpUtils.GpReverse((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

    }
}