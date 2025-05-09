using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public class Float64RandomComposer
{
    public Random RandomGenerator { get; }

    public double MinScalarValue { get; private set; } = -1d;

    public double MaxScalarValue { get; private set; } = 1d;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RandomComposer()
    {
        RandomGenerator = new Random();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RandomComposer(int seed)
    {
        RandomGenerator = new Random(seed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RandomComposer(Random randomGenerator)
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
        return MinScalarValue + RandomGenerator.NextDouble() * (MaxScalarValue - MinScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarValue(double minValue, double maxValue)
    {
        return minValue + RandomGenerator.NextDouble() * (maxValue - minValue);
    }
}