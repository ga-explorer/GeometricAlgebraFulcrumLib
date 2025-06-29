﻿using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

// TODO: Move this to GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra
public class ScalarArrayColumn<T> :
    IReadOnlyList<T>
{
    public T[,] ScalarArray { get; }

    public int ColumnIndex { get; }

    public int Count { get; }

    public T this[int index]
        => ScalarArray[index, ColumnIndex];


    
    public ScalarArrayColumn(T[,] scalarArray, int columnIndex)
    {
        if (columnIndex < 0 || columnIndex > scalarArray.GetLength(1))
            throw new ArgumentOutOfRangeException(nameof(columnIndex));

        ScalarArray = scalarArray;
        ColumnIndex = columnIndex;
        Count = scalarArray.GetLength(0);
    }

    
    public IEnumerator<T> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[i, ColumnIndex])
            .GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}