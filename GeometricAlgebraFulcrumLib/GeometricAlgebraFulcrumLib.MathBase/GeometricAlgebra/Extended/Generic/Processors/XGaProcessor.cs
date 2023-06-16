using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors
{
    public class XGaProcessor<T> :
        XGaMetric
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaEuclideanProcessor<T> CreateEuclidean(IScalarProcessor<T> scalarProcessor)
        {
            return new XGaEuclideanProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaProjectiveProcessor<T> CreateProjective(IScalarProcessor<T> scalarProcessor)
        {
            return new XGaProjectiveProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaConformalProcessor<T> CreateConformal(IScalarProcessor<T> scalarProcessor)
        {
            return new XGaConformalProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
        {
            if (negativeCount == 0 && zeroCount == 0)
                return CreateEuclidean(scalarProcessor);

            if (negativeCount == 0 && zeroCount == 1)
                return CreateProjective(scalarProcessor);

            if (negativeCount == 1 && zeroCount == 0)
                return CreateConformal(scalarProcessor);

            return new XGaProcessor<T>(
                scalarProcessor,
                negativeCount,
                zeroCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaProcessor<T> Create(IScalarProcessor<T> scalarProcessor, XGaMetric metric)
        {
            if (metric.IsEuclidean)
                return CreateEuclidean(scalarProcessor);

            if (metric.IsProjective)
                return CreateProjective(scalarProcessor);

            if (metric.IsConformal)
                return CreateConformal(scalarProcessor);

            return new XGaProcessor<T>(
                scalarProcessor,
                metric.NegativeSignatureBasisCount,
                metric.ZeroSignatureBasisCount
            );
        }


        public IScalarProcessor<T> ScalarProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaProcessor(IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
            : base(negativeCount, zeroCount)
        {
            ScalarProcessor = scalarProcessor;
        }
    }
}
