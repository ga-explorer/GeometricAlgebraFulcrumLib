﻿using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;

public class ScalarArrayRow<T> :
    IReadOnlyList<T>
{
    public T[,] ScalarArray { get; }

    public int RowIndex { get; }

    public int Count { get; }

    public T this[int index]
        => ScalarArray[RowIndex, index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArrayRow(T[,] scalarArray, int rowIndex)
    {
        if (rowIndex < 0 || rowIndex > scalarArray.GetLength(0))
            throw new ArgumentOutOfRangeException(nameof(rowIndex));

        ScalarArray = scalarArray;
        RowIndex = rowIndex;
        Count = scalarArray.GetLength(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[RowIndex, i])
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}