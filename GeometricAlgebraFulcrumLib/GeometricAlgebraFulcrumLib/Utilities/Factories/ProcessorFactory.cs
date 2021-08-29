using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaProcessor<T> CreateLaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorEuclidean<T> CreateGaEuclideanProcessor<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorEuclidean<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateGaConformalProcessor<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateConformal(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateGaProjectiveProcessor<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.CreateProjective(vSpaceDimension)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateGaOrthonormalProcessor<T>(this IScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaProcessorOrthonormal<T> CreateGaOrthonormalProcessor<T>(this IScalarProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GaProcessorOrthonormal<T>(
                scalarProcessor, 
                GaSignatureFactory.Create(positiveCount, negativeCount, zeroCount)
            );
        }
    }
}