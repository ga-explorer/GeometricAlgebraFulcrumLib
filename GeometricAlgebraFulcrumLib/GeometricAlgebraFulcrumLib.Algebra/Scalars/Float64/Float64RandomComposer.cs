using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

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
        return RandomGenerator.GetFloat64(MinScalarValue, MaxScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarValue(double minValue, double maxValue)
    {
        return RandomGenerator.GetFloat64(minValue, maxValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetScalars(int count)
    {
        return Enumerable
            .Range(0, count)
            .Select(_ => GetScalarValue());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray1D(int size)
    {
        return RandomGenerator.GetFloat64Array1D(size, MinScalarValue, MaxScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D(int rowsCount, int columnsCount)
    {
        return RandomGenerator.GetFloat64Array2D(rowsCount, columnsCount, MinScalarValue, MaxScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix GetDenseMatrix(int rowsCount, int columnsCount)
    {
        return RandomGenerator.GetRealDenseMatrix(rowsCount, columnsCount, MinScalarValue, MaxScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MathNet.Numerics.Permutation GetPermutation(int size)
    {
        return RandomGenerator.GetPermutation(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetPermutationArray2D(int size)
    {
        return RandomGenerator.GetPermutationArray2D(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseMatrix GetPermutationMatrix(int size)
    {
        return RandomGenerator.GetPermutationMatrix(size);
    }
}