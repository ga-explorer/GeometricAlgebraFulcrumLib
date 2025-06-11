using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public class Float64ArrayRow :
    IReadOnlyList<double>
{
    public double[,] ScalarArray { get; }

    public int RowIndex { get; }

    public int Count { get; }

    public double this[int index]
        => ScalarArray[RowIndex, index];


    
    public Float64ArrayRow(double[,] scalarArray, int rowIndex)
    {
        if (rowIndex < 0 || rowIndex > scalarArray.GetLength(0))
            throw new ArgumentOutOfRangeException(nameof(rowIndex));

        ScalarArray = scalarArray;
        RowIndex = rowIndex;
        Count = scalarArray.GetLength(1);
    }

    
    public IEnumerator<double> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[RowIndex, i])
            .GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}