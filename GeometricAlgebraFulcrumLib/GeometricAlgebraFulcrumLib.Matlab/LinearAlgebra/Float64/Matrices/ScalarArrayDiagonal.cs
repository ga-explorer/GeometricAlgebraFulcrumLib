using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public class ScalarArrayDiagonal<T> :
    IReadOnlyList<T>
{
    public T[,] ScalarArray { get; }

    public int Count { get; }

    public T this[int index]
        => ScalarArray[index, index];


    
    public ScalarArrayDiagonal(T[,] scalarArray)
    {
        ScalarArray = scalarArray;

        Count = Math.Min(
            ScalarArray.GetLength(0),
            ScalarArray.GetLength(1)
        );
    }

    
    public IEnumerator<T> GetEnumerator()
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