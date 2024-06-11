using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarProcessorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarComposer<T> CreateScalarComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return ScalarComposer<T>.Create(scalarProcessor);
    }

    //public static bool ValidateEqual<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T scalar2)
    //{
    //    scalarProcessor.Subtract(scalar1, scalar2).
    //}

    public static T[] CreateArrayZero1D<T>(this IScalarProcessor<T> scalarProcessor, int count)
    {
        var exprArray = new T[count];

        for (var i = 0; i < count; i++)
            exprArray[i] = scalarProcessor.ZeroValue;

        return exprArray;
    }

    public static T[,] CreateArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count)
    {
        var exprArray = new T[count, count];

        for (var i = 0; i < count; i++)
        for (var j = 0; j < count; j++)
            exprArray[i, j] = scalarProcessor.ZeroValue;

        return exprArray;
    }

    public static T[,] CreateArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count1, int count2)
    {
        var exprArray = new T[count1, count2];

        for (var i = 0; i < count1; i++)
        for (var j = 0; j < count2; j++)
            exprArray[i, j] = scalarProcessor.ZeroValue;

        return exprArray;
    }

    public static T[,] CreateArrayIdentity2D<T>(this IScalarProcessor<T> scalarProcessor, int size)
    {
        var matrix = new T[size, size];

        for (var i = 0; i < size; i++)
        {
            matrix[i, i] = scalarProcessor.OneValue;

            for (var j = 0; j < i; j++)
                matrix[i, j] = scalarProcessor.ZeroValue;

            for (var j = i + 1; j < size; j++)
                matrix[i, j] = scalarProcessor.ZeroValue;
        }

        return matrix;
    }
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.ScalarFromText(scalar).Negative();
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IScalarProcessor<TValue> scalarProcessor, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
    {
        return keyValuePairs.Select(
            pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value).ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<TKey, TValue>> Negative<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IScalarProcessor<TValue> scalarProcessor)
    {
        return keyValuePairs.Select(
            pair => new KeyValuePair<TKey, TValue>(pair.Key, scalarProcessor.Negative(pair.Value).ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> BinomialCoefficient<T>(this IScalarProcessor<T> scalarProcessor, int setSize, int subsetSize)
    {
        return scalarProcessor.ScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> BinomialCoefficient<T>(this IScalarProcessor<T> scalarProcessor, uint setSize, uint subsetSize)
    {
        return scalarProcessor.ScalarFromNumber(setSize.GetBinomialCoefficient(subsetSize));
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sinc<T>(this IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        var scalar = scalarProcessor.Positive(scalarValue);

        if (scalar.IsZero()) return scalarProcessor.Zero;

        return scalar.IsPositive() ? scalar : scalar.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> BoxCar<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T scalarA, T scalarB)
    {
        return scalarProcessor.Subtract(scalar, scalarA).UnitStep() -
               scalarProcessor.Subtract(scalar, scalarB).UnitStep();
    }


}