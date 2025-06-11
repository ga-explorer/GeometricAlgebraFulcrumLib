using System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

public class Float64RandomComposer
{
    public Random RandomGenerator { get; }

    public double MinScalarValue { get; private set; } = -1d;

    public double MaxScalarValue { get; private set; } = 1d;


    
    public Float64RandomComposer()
    {
        RandomGenerator = new Random();
    }

    
    public Float64RandomComposer(int seed)
    {
        RandomGenerator = new Random(seed);
    }

    
    public Float64RandomComposer(Random randomGenerator)
    {
        RandomGenerator = randomGenerator;
    }


    
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

    
    public double GetScalarValue()
    {
        return RandomGenerator.GetFloat64(MinScalarValue, MaxScalarValue);
    }

    
    public double GetScalarValue(double minValue, double maxValue)
    {
        return RandomGenerator.GetFloat64(minValue, maxValue);
    }
}