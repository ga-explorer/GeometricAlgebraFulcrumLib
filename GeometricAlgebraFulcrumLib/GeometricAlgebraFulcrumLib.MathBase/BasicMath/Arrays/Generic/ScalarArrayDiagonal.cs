using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Generic;

public class ScalarArrayDiagonal<T> :
    IReadOnlyList<T>
{
    public T[,] ScalarArray { get; }

    public int Count { get; }

    public T this[int index]
        => ScalarArray[index, index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarArrayDiagonal(T[,] scalarArray)
    {
        ScalarArray = scalarArray;

        Count = Math.Min(
            ScalarArray.GetLength(0),
            ScalarArray.GetLength(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[i, i])
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}