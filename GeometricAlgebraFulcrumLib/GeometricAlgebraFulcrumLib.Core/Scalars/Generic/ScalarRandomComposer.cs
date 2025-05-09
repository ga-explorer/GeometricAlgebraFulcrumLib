using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public class ScalarRandomComposer<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }

    public Random RandomGenerator { get; }

    public double MinScalarValue { get; private set; } = -1d;

    public double MaxScalarValue { get; private set; } = 1d;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
        RandomGenerator = new Random();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor, int seed)
    {
        ScalarProcessor = scalarProcessor;
        RandomGenerator = new Random(seed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRandomComposer(IScalarProcessor<T> scalarProcessor, Random randomGenerator)
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
    public Scalar<T> GetScalar()
    {
        return ScalarProcessor.ScalarFromRandom(
            RandomGenerator,
            MinScalarValue,
            MaxScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalar(double minValue, double maxValue)
    {
        return ScalarProcessor.ScalarFromRandom(
            RandomGenerator,
            minValue,
            maxValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetScalarValues(int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(_ => GetScalar().ScalarValue);
    }

    public T[,] GetScalarValuesArray(int rowsCount, int columnsCount)
    {
        var array = new T[rowsCount, columnsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < columnsCount; j++)
            array[i, j] = GetScalar().ScalarValue;

        return array;
    }

    public T[,] GetPermutationArray(int size)
    {
        var array = new T[size, size];

        var indexList = Enumerable
            .Range(0, size)
            .Shuffled(RandomGenerator);

        var i = 0;
        foreach (var colIndex in indexList)
        {
            for (var j = 0; j < size; j++)
                array[i, j] = j == colIndex
                    ? ScalarProcessor.OneValue
                    : ScalarProcessor.ZeroValue;

            i++;
        }

        return array;
    }
}