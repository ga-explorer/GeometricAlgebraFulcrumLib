using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra
{
    public class ScalarRandomComposer<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public System.Random RandomGenerator { get; }

        public double MinScalarValue { get; private set; } = -1d;

        public double MaxScalarValue { get; private set; } = 1d;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = new System.Random();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor, int seed)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = new System.Random(seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor, System.Random randomGenerator)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = randomGenerator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetScalarLimits(double minScalarValue, double maxScalarValue)
        {
            if (minScalarValue.IsNaNOrInfinite() || maxScalarValue.IsNaNOrInfinite())
                throw new ArgumentException();

            if (minScalarValue <= maxScalarValue)
            {
                MinScalarValue = minScalarValue;
                MaxScalarValue = maxScalarValue;
            }
            else
            {
                MinScalarValue = maxScalarValue;
                MaxScalarValue = minScalarValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalarValue()
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator,
                MinScalarValue,
                MaxScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalarValue(double minValue, double maxValue)
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator,
                minValue,
                maxValue
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Scalar<T> GetScalar()
        //{
        //    return ScalarProcessor.CreateScalar(
        //        GetScalarValue()
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Scalar<T> GetScalar(double minValue, double maxValue)
        //{
        //    return ScalarProcessor.CreateScalar(
        //        GetScalarValue(minValue, maxValue)
        //    );
        //}
    }
}