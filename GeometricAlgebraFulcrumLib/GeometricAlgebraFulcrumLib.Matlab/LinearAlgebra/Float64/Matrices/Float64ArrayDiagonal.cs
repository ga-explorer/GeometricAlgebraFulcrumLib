using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public class Float64ArrayDiagonal :
    IReadOnlyList<double>
{
    public double[,] ScalarArray { get; }

    public int Count { get; }

    public double this[int index]
        => ScalarArray[index, index];


    
    public Float64ArrayDiagonal(double[,] scalarArray)
    {
        ScalarArray = scalarArray;

        Count = Math.Min(
            ScalarArray.GetLength(0),
            ScalarArray.GetLength(1)
        );
    }

    
    public IEnumerator<double> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[i, i])
            .GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}