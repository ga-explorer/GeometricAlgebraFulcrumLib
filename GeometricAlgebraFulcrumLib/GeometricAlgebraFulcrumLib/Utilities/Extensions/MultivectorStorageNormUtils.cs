﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageNormUtils
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Norm<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor.SqrtOfAbs(
                NormSquared(processor, mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T NormSquared<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ENormSquared(mv1),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.NormSquared(ortProcessor, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).NormSquared(mv1)
            };
        }
    }
}