using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public static class GaProductHipUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Hip<T>(this IGaProcessor<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EHip(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtHipUtils.Hip(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobHipUtils.Hip((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Hip<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessor<T> processor)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    mv1.EHip(mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    GaProductOrtHipUtils.Hip(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobHipUtils.Hip((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }

    }
}