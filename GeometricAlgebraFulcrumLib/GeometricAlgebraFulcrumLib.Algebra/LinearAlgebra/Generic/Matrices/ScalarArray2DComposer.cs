using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

public sealed class ScalarArray2DComposer<T>
{
    public int RowCount
        => ScalarArray.GetLength(0);

    public int ColumnCount
        => ScalarArray.GetLength(1);

    public T[,] ScalarArray { get; private set; }

    public IScalarProcessor<T> ScalarProcessor { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ScalarArray2DComposer(IScalarProcessor<T> scalarProcessor, int rowCount, int colCount)
    {
        ScalarProcessor = scalarProcessor;

        ScalarArray = scalarProcessor.CreateArrayZero2D(
            rowCount,
            colCount
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Clear()
    {
        ScalarArray = ScalarProcessor.CreateArrayZero2D(
            RowCount,
            ColumnCount
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Clear(int rowCount, int colCount)
    {
        ScalarArray = ScalarProcessor.CreateArrayZero2D(
            rowCount,
            colCount
        );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> SetTerm(int index1, int index2, T scalar)
    {
        if (!ScalarProcessor.IsValid(scalar))
            throw new InvalidOperationException();

        ScalarArray[index1, index2] = scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> SetTerm(int index1, int index2, IScalar<T> scalar)
    {
        return SetTerm(index1, index2, scalar.ScalarValue);
    }

    public ScalarArray2DComposer<T> SetTerms(IEnumerable<KeyValuePair<IndexPair, T>> indexScalarPairs)
    {
        foreach (var ((index1, index2), scalar) in indexScalarPairs)
            SetTerm(index1, index2, scalar);

        return this;
    }

    public ScalarArray2DComposer<T> SetRow(int index1, IReadOnlyList<T> vector)
    {
        for (var index2 = 0; index2 < vector.Count; index2++)
            SetTerm(index1, index2, vector[index2]);

        return this;
    }

    public ScalarArray2DComposer<T> SetRow(int index1, IReadOnlyList<T> vector, T scalingFactor)
    {
        for (var index2 = 0; index2 < vector.Count; index2++)
            SetTerm(index1, index2, ScalarProcessor.Times(vector[index2], scalingFactor));

        return this;
    }

    public ScalarArray2DComposer<T> SetRow(int index1, IEnumerable<KeyValuePair<int, T>> vector)
    {
        foreach (var (index2, scalar) in vector)
            SetTerm(index1, index2, scalar);

        return this;
    }

    public ScalarArray2DComposer<T> SetRow(int index1, IEnumerable<KeyValuePair<int, T>> vector, T scalingFactor)
    {
        foreach (var (index2, scalar) in vector)
            SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }

    public ScalarArray2DComposer<T> SetColumn(int index2, IReadOnlyList<T> vector)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SetTerm(index1, index2, vector[index1]);

        return this;
    }

    public ScalarArray2DComposer<T> SetColumn(int index2, IReadOnlyList<T> vector, T scalingFactor)
    {
        for (var index1 = 0; index1 < vector.Count; index1++)
            SetTerm(index1, index2, ScalarProcessor.Times(vector[index1], scalingFactor));

        return this;
    }

    public ScalarArray2DComposer<T> SetColumn(int index2, IEnumerable<KeyValuePair<int, T>> vector)
    {
        foreach (var (index1, scalar) in vector)
            SetTerm(index1, index2, scalar);

        return this;
    }

    public ScalarArray2DComposer<T> SetColumn(int index2, IEnumerable<KeyValuePair<int, T>> vector, T scalingFactor)
    {
        foreach (var (index1, scalar) in vector)
            SetTerm(index1, index2, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> AddTerm(int index1, int index2, T scalar)
    {
        if (!ScalarProcessor.IsValid(scalar))
            throw new InvalidOperationException();

        ScalarArray[index1, index2] =
            ScalarProcessor.Add(
                ScalarArray[index1, index2],
                scalar
            ).ScalarValue;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> SubtractTerm(int index1, int index2, T scalar)
    {
        if (!ScalarProcessor.IsValid(scalar))
            throw new InvalidOperationException();

        ScalarArray[index1, index2] =
            ScalarProcessor.Subtract(
                ScalarArray[index1, index2],
                scalar
            ).ScalarValue;

        return this;
    }


    public ScalarArray2DComposer<T> MapScalars(Func<T, T> scalarMapping)
    {
        for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
            {
                var scalar = scalarMapping(ScalarArray[i, j])
                             ?? ScalarProcessor.ZeroValue;

                if (!ScalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                ScalarArray[i, j] = scalar;
            }

        return this;
    }

    public ScalarArray2DComposer<T> MapScalars(Func<int, int, T, T> scalarMapping)
    {
        for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
            {
                var scalar = scalarMapping(i, j, ScalarArray[i, j])
                             ?? ScalarProcessor.ZeroValue;

                if (!ScalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                ScalarArray[i, j] = scalar;
            }

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Negative()
    {
        return MapScalars(scalar => ScalarProcessor.Negative(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Times(T scalingFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalingFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Times(T[,] scalarArray)
    {
        ScalarArray = ScalarProcessor.Times(ScalarArray, scalarArray);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Divide(T scalingFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalingFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArray2DComposer<T> Transpose()
    {
        ScalarArray = ScalarArray.Transpose();

        return this;
    }
}