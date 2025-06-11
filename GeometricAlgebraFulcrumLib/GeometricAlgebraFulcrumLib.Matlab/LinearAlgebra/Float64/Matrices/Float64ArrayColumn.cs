using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public class Float64ArrayColumn :
    IReadOnlyList<double>

{
    public double[,] ScalarArray { get; }

    public int ColumnIndex { get; }

    public int Count { get; }

    public double this[int index]
        => ScalarArray[index, ColumnIndex];


    
    public Float64ArrayColumn(double[,] scalarArray, int columnIndex)
    {
        if (columnIndex < 0 || columnIndex > scalarArray.GetLength(1))
            throw new ArgumentOutOfRangeException(nameof(columnIndex));

        ScalarArray = scalarArray;
        ColumnIndex = columnIndex;
        Count = scalarArray.GetLength(0);
    }

    
    public IEnumerator<double> GetEnumerator()
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