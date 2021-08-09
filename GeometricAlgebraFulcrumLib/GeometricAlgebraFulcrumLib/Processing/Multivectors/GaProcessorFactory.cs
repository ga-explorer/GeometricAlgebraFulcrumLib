using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public static class GaProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericEuclidean<T> CreateEuclideanProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericEuclidean<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateConformalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateConformal(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateProjectiveProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateProjective(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorGenericOrthonormal<T> CreateOrthonormalProcessor<T>(this IGaScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GaProcessorGenericOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount, zeroCount)
            );
        }
    }
}