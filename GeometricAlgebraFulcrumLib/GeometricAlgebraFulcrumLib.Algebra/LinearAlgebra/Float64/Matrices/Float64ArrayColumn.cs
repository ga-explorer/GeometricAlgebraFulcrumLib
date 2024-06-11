using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

public class Float64ArrayColumn :
    IReadOnlyList<double>

{
    public double[,] ScalarArray { get; }

    public int ColumnIndex { get; }

    public int Count { get; }

    public double this[int index]
        => ScalarArray[index, ColumnIndex];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ArrayColumn(double[,] scalarArray, int columnIndex)
    {
        if (columnIndex < 0 || columnIndex > scalarArray.GetLength(1))
            throw new ArgumentOutOfRangeException(nameof(columnIndex));

        ScalarArray = scalarArray;
        ColumnIndex = columnIndex;
        Count = scalarArray.GetLength(0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        return Count
            .GetRange(i => ScalarArray[i, ColumnIndex])
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}