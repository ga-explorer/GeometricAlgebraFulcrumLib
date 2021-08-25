using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public static class GaProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorEuclidean<T> CreateEuclideanProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorEuclidean<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateConformalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateConformal(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateProjectiveProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateProjective(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount, zeroCount)
            );
        }
    }
}