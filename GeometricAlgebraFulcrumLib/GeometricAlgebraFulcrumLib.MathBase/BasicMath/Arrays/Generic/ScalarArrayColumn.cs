using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Generic
{
    public class ScalarArrayColumn<T> :
        IReadOnlyList<T>

    {
        public T[,] ScalarArray { get; }

        public int ColumnIndex { get; }

        public int Count { get; }

        public T this[int index]
            => ScalarArray[index, ColumnIndex];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarArrayColumn(T[,] scalarArray, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > scalarArray.GetLength(1))
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            ScalarArray = scalarArray;
            ColumnIndex = columnIndex;
            Count = scalarArray.GetLength(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
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
}
