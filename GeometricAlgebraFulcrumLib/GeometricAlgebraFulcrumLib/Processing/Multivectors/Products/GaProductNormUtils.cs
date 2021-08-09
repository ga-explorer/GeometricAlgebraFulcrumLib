using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductNormUtils
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor.SqrtOfAbs(
                NormSquared(processor, mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.ENormSquared(mv1),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.NormSquared(ortProcessor, mv1),

                _ =>
                    GaProductCobNormUtils.NormSquared((IGaProcessorChangeOfBasis<T>) processor, mv1)
            };
        }
    }
}