using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductNormUtils
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor.SqrtOfAbs(
                NormSquared(processor, mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ENormSquared(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtNormUtils.NormSquared(ortProcessor, mv1),

                _ =>
                    GaProductCobNormUtils.NormSquared((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor.SqrtOfAbs(
                NormSquared(processor, mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGasMultivector<T> mv1, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.ENormSquared(),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtNormUtils.NormSquared(ortProcessor, mv1),

                _ =>
                    GaProductCobNormUtils.NormSquared((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }

    }
}