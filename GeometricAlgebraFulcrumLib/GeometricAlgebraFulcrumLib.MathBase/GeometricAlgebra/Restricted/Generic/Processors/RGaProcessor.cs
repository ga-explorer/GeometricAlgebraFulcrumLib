using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors
{
    public class RGaProcessor<T> :
        RGaMetric
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaEuclideanProcessor<T> CreateEuclidean(IScalarProcessor<T> scalarProcessor)
        {
            return new RGaEuclideanProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaProjectiveProcessor<T> CreateProjective(IScalarProcessor<T> scalarProcessor)
        {
            return new RGaProjectiveProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaConformalProcessor<T> CreateConformal(IScalarProcessor<T> scalarProcessor)
        {
            return new RGaConformalProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
        {
            if (negativeCount == 0 && zeroCount == 0)
                return CreateEuclidean(scalarProcessor);

            if (negativeCount == 0 && zeroCount == 1)
                return CreateProjective(scalarProcessor);

            if (negativeCount == 1 && zeroCount == 0)
                return CreateConformal(scalarProcessor);

            return new RGaProcessor<T>(
                scalarProcessor,
                negativeCount,
                zeroCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, RGaMetric metric)
        {
            if (metric.IsEuclidean)
                return CreateEuclidean(scalarProcessor);

            if (metric.IsProjective)
                return CreateProjective(scalarProcessor);

            if (metric.IsConformal)
                return CreateConformal(scalarProcessor);

            return new RGaProcessor<T>(
                scalarProcessor,
                metric.NegativeSignatureBasisCount,
                metric.ZeroSignatureBasisCount
            );
        }


        public IScalarProcessor<T> ScalarProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaProcessor(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
            : base(negativeCount, zeroCount)
        {
            ScalarProcessor = scalarProcessor;
        }
    }
}
