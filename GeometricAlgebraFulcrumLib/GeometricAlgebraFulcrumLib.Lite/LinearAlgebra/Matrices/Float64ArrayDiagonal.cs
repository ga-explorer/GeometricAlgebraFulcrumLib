using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;

public class Float64ArrayDiagonal :
    IReadOnlyList<double>
{
    public double[,] ScalarArray { get; }

    public int Count { get; }

    public double this[int index]
        => ScalarArray[index, index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ArrayDiagonal(double[,] scalarArray)
    {
        ScalarArray = scalarArray;

        Count = Math.Min(
            ScalarArray.GetLength(0),
            ScalarArray.GetLength(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
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