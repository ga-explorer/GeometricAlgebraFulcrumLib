using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ESp(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtSpUtils.Sp(ortProcessor, mv1),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ESp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtSpUtils.Sp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ESp(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtSpUtils.Sp(ortProcessor, mv1),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ESp(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtSpUtils.Sp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobSpUtils.Sp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

    }
}