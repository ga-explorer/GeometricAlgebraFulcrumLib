﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.ChangeOfBasis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Rcp<T>(this IGaProcessor<T> processor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return processor switch
            {
                IGaProcessorEuclidean<T> _ =>
                    processor.ERcp(mv1, mv2),
                
                IGaProcessorOrthonormal<T> ortProcessor =>
                    processor.Rcp(ortProcessor, mv1, mv2),

                _ =>
                    GaProductCobRcpUtils.Rcp((IGaProcessorChangeOfBasis<T>) processor, mv1, mv2)
            };
        }
    }
}