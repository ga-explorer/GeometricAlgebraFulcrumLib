using System.Runtime.CompilerServices;
using DataStructuresLib.Random;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra
{
    public class RandomComposerFloat64
    {
        public System.Random RandomGenerator { get; }

        public double MinScalarValue { get; private set; } = -1d;

        public double MaxScalarValue { get; private set; } = 1d;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RandomComposerFloat64()
        {
            RandomGenerator = new System.Random();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RandomComposerFloat64(int seed)
        {
            RandomGenerator = new System.Random(seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RandomComposerFloat64(System.Random randomGenerator)
        {
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
        public double GetScalarValue()
        {
            return RandomGenerator.GetNumber(MinScalarValue, MaxScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarValue(double minValue, double maxValue)
        {
            return RandomGenerator.GetNumber(minValue, maxValue);
        }
    }
}