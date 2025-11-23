using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

public static class ArrayExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSquare<T>(this T[,] itemArray)
    {
        return itemArray.GetLength(0) == itemArray.GetLength(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Size<T>(this T[,] itemArray)
    {
        var n = itemArray.GetLength(0);

        return n == itemArray.GetLength(1) 
            ? n : throw new ArgumentException("Array must be square");
    }


    public static T[] Concat<T>(this T[] source1, IEnumerable<T> source2)
    {
        return ((IEnumerable<T>)source1).Concat(source2).ToArray();
    }

    /// <summary>
    /// Copies a range of the specified <see cref="Array" />.
    /// </summary>
    /// <typeparam name="T">The type of the array items.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="from">The start index.</param>
    /// <param name="to">The end index.</param>
    /// <returns>An <see cref="Array" /> containing the items from index <paramref name="from" /> to index <paramref name="to" />.</returns>
    public static T[] CopyOfRange<T>(this T[] source, int from, int to)
    {
        var result = new T[to - from];
        for (var i = from; i < Math.Min(source.Length, to); i++)
        {
            result[i - from] = source[i];
        }

        return result;
    }

    /// <summary>
    /// Copies the first items of the specified <see cref="Array" />.
    /// </summary>
    /// <typeparam name="T">The type of the array items.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="newLength">The number of items to copy.</param>
    /// <returns>An <see cref="Array" /> containing the items from index 0 to index <paramref name="newLength" />.</returns>
    public static T[] CopyOf<T>(this T[] source, int newLength)
    {
        var result = new T[newLength];
        for (var i = 0; i < Math.Min(source.Length, newLength); i++)
        {
            result[i] = source[i];
        }

        return result;
    }

    /// <summary>
    /// Fills the specified array with values in the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the array items.</typeparam>
    /// <param name="source">The source array.</param>
    /// <param name="i0">The start index.</param>
    /// <param name="i1">The end index.</param>
    /// <param name="v">The value to fill.</param>
    public static void Fill<T>(this T[] source, int i0, int i1, T v)
    {
        for (var i = i0; i < i1; i++)
        {
            source[i] = v;
        }
    }


    public static T[] ToArray1D<T>(this T[,] array, bool columnMajorOrder = true)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        var result = new T[rows * cols];

        var k = 0;

        if (columnMajorOrder)
        {
            for (var j = 0; j < cols; j++)
                for (var i = 0; i < rows; i++)
                {
                    result[k] = array[i, j];
                    k++;
                }
        }
        else
        {
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                {
                    result[k] = array[i, j];
                    k++;
                }
        }

        return result;
    }

    public static T[] RowToArray1D<T>(this T[,] arrayIn, int rowIndex)
    {
        var n2 = arrayIn.GetLength(1);

        var arrayOut = new T[n2];
        for (var j = 0; j < n2; j++)
            arrayOut[j] = arrayIn[rowIndex, j];

        return arrayOut;
    }
    
    public static IEnumerable<T[]> RowsToArrays1D<T>(this T[,] arrayIn)
    {
        return arrayIn
            .GetLength(0)
            .GetRange(arrayIn.RowToArray1D);
    }

    public static T[] ColumnToArray1D<T>(this T[,] arrayIn, int columnIndex)
    {
        var n1 = arrayIn.GetLength(0);

        var arrayOut = new T[n1];
        for (var i = 0; i < n1; i++)
            arrayOut[i] = arrayIn[i, columnIndex];

        return arrayOut;
    }
    
    public static IEnumerable<T[]> ColumnsToArrays1D<T>(this T[,] arrayIn)
    {
        return arrayIn
            .GetLength(1)
            .GetRange(arrayIn.ColumnToArray1D);
    }


    public static List<T> ToList<T>(this T[,] array, bool columnMajorOrder = true)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        var result = new List<T>(rows * cols);

        if (columnMajorOrder)
        {
            for (var j = 0; j < cols; j++)
                for (var i = 0; i < rows; i++)
                    result.Add(array[i, j]);
        }
        else
        {
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    result.Add(array[i, j]);
        }

        return result;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewRow<T>(this T[,] array, int rowIndex)
    {
        return Array2DAsReadOnlyList<T>.Row(array, rowIndex);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<TIn, TOut> ViewRow<TIn, TOut>(this TIn[,] array, Func<TIn, TOut> itemMap, int rowIndex)
    {
        return Array2DAsReadOnlyList<TIn, TOut>.Row(array, itemMap, rowIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewColumn<T>(this T[,] array, int colIndex)
    {
        return Array2DAsReadOnlyList<T>.Column(array, colIndex);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewDiagonal<T>(this T[,] array)
    {
        return Array2DAsReadOnlyList<T>.Diagonal(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewSubDiagonal<T>(this T[,] array, int offset)
    {
        return Array2DAsReadOnlyList<T>.SubDiagonal(array, offset);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewFullRowMajor<T>(this T[,] array)
    {
        return Array2DAsReadOnlyList<T>.FullRowMajor(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewFullColumnMajor<T>(this T[,] array)
    {
        return Array2DAsReadOnlyList<T>.FullColumnMajor(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewItems<T>(this T[,] array, params (int, int)[] indexPairs)
    {
        return Array2DAsReadOnlyList<T>.Create(array, indexPairs);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewItems<T>(this T[,] array, IReadOnlyList<IPair<int>> indexPairList)
    {
        return Array2DAsReadOnlyList<T>.Create(array, indexPairList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Array2DAsReadOnlyList<T> ViewItems<T>(this T[,] array, int count, Func<int, Pair<int>> indexToIndexPairMap)
    {
        return Array2DAsReadOnlyList<T>.Create(array, count, indexToIndexPairMap);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Array2DAsReadOnlyList<T>> ViewRows<T>(this T[,] array)
    {
        return array
            .GetLength(0)
            .GetRange(array.ViewRow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Array2DAsReadOnlyList<double>> ViewColumns(this double[,] array)
    {
        return array
            .GetLength(1)
            .GetRange(array.ViewRow);
    }


    public static IEnumerable<T> GetItems<T>(this T[,] array, bool columnMajorOrder = true)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        if (columnMajorOrder)
        {
            for (var j = 0; j < cols; j++)
                for (var i = 0; i < rows; i++)
                    yield return array[i, j];
        }
        else
        {
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    yield return array[i, j];
        }
    }

    public static IEnumerable<T1> GetItems<T, T1>(this T[,] array, Func<T, T1> mappingFunc, bool columnMajorOrder = true)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        if (columnMajorOrder)
        {
            for (var j = 0; j < cols; j++)
                for (var i = 0; i < rows; i++)
                    yield return mappingFunc(array[i, j]);
        }
        else
        {
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    yield return mappingFunc(array[i, j]);
        }
    }

    public static IEnumerable<T> GetRowItems<T>(this T[,] array, int rowIndex)
    {
        return Enumerable
            .Range(0, array.GetLength(1))
            .Select(i => array[rowIndex, i]);
    }

    public static IEnumerable<T1> GetRowItems<T, T1>(this T[,] array, Func<T, T1> mappingFunc, int rowIndex)
    {
        return Enumerable
            .Range(0, array.GetLength(1))
            .Select(i => mappingFunc(array[rowIndex, i]));
    }

    public static IEnumerable<T> GetColumnItems<T>(this T[,] array, int columnIndex)
    {
        return Enumerable
            .Range(0, array.GetLength(0))
            .Select(i => array[i, columnIndex]);
    }

    public static IEnumerable<T1> GetColumnItems<T, T1>(this T[,] array, Func<T, T1> mappingFunc, int columnIndex)
    {
        return Enumerable
            .Range(0, array.GetLength(0))
            .Select(i => mappingFunc(array[i, columnIndex]));
    }


    public static IEnumerable<Tuple<int, T>> GetIndexItemTuples<T>(this T[] array)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
            yield return Tuple.Create(i, array[i]);
    }

    public static IEnumerable<Tuple<int, T1>> GetIndexItemTuples<T, T1>(this T[] array, Func<T, T1> mappingFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
            yield return Tuple.Create(i, mappingFunc(array[i]));
    }

    public static IEnumerable<Tuple<int, T>> GetIndexItemTuples<T>(this T[] array, Func<T, bool> selectionFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
        {
            var item = array[i];

            if (selectionFunc(item))
                yield return Tuple.Create(i, item);
        }
    }

    public static IEnumerable<Tuple<int, T1>> GetIndexItemTuples<T, T1>(this T[] array, Func<T, T1> mappingFunc, Func<T, bool> selectionFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
        {
            var item = array[i];

            if (selectionFunc(item))
                yield return Tuple.Create(i, mappingFunc(item));
        }
    }

    public static IEnumerable<Tuple<int, T>> GetIndexItemTuples<T>(this T[] array, Func<int, T, bool> selectionFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
        {
            var item = array[i];

            if (selectionFunc(i, item))
                yield return Tuple.Create(i, item);
        }
    }

    public static IEnumerable<Tuple<int, T1>> GetIndexItemTuples<T, T1>(this T[] array, Func<T, T1> mappingFunc, Func<int, T, bool> selectionFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
        {
            var item = array[i];

            if (selectionFunc(i, item))
                yield return Tuple.Create(i, mappingFunc(item));
        }
    }


    public static IEnumerable<Tuple<int, int, T>> GetIndexItemTuples<T>(this T[,] array)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                yield return Tuple.Create(i, j, array[i, j]);
    }

    public static IEnumerable<Tuple<int, int, T1>> GetIndexItemTuples<T, T1>(this T[,] array, Func<T, T1> mappingFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                yield return Tuple.Create(i, j, mappingFunc(array[i, j]));
    }

    public static IEnumerable<Tuple<int, int, T>> GetIndexItemTuples<T>(this T[,] array, Func<T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
            {
                var item = array[i, j];

                if (selectionFunc(item))
                    yield return Tuple.Create(i, j, item);
            }
    }

    public static IEnumerable<Tuple<int, int, T1>> GetIndexItemTuples<T, T1>(this T[,] array, Func<T, T1> mappingFunc, Func<T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
            {
                var item = array[i, j];

                if (selectionFunc(item))
                    yield return Tuple.Create(i, j, mappingFunc(item));
            }
    }

    public static IEnumerable<Tuple<int, int, T>> GetIndexItemTuples<T>(this T[,] array, Func<int, int, T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
            {
                var item = array[i, j];

                if (selectionFunc(i, j, item))
                    yield return Tuple.Create(i, j, item);
            }
    }

    public static IEnumerable<Tuple<int, int, T1>> GetIndexItemTuples<T, T1>(this T[,] array, Func<T, T1> mappingFunc, Func<int, int, T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
            {
                var item = array[i, j];

                if (selectionFunc(i, j, item))
                    yield return Tuple.Create(i, j, mappingFunc(item));
            }
    }


    public static IEnumerable<Tuple<int, int, int, T>> GetIndexItemTuples<T>(this T[,,] array)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                    yield return Tuple.Create(i, j, k, array[i, j, k]);
    }

    public static IEnumerable<Tuple<int, int, int, T1>> GetIndexItemTuples<T, T1>(this T[,,] array, Func<T, T1> mappingFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                    yield return Tuple.Create(i, j, k, mappingFunc(array[i, j, k]));
    }

    public static IEnumerable<Tuple<int, int, int, T>> GetIndexItemTuples<T>(this T[,,] array, Func<T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                {
                    var item = array[i, j, k];

                    if (selectionFunc(item))
                        yield return Tuple.Create(i, j, k, item);
                }
    }

    public static IEnumerable<Tuple<int, int, int, T1>> GetIndexItemTuples<T, T1>(this T[,,] array, Func<T, T1> mappingFunc, Func<T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                {
                    var item = array[i, j, k];

                    if (selectionFunc(item))
                        yield return Tuple.Create(i, j, k, mappingFunc(item));
                }
    }

    public static IEnumerable<Tuple<int, int, int, T>> GetIndexItemTuples<T>(this T[,,] array, Func<int, int, int, T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                {
                    var item = array[i, j, k];

                    if (selectionFunc(i, j, k, item))
                        yield return Tuple.Create(i, j, k, item);
                }
    }

    public static IEnumerable<Tuple<int, int, int, T1>> GetIndexItemTuples<T, T1>(this T[,,] array, Func<T, T1> mappingFunc, Func<int, int, int, T, bool> selectionFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                {
                    var item = array[i, j, k];

                    if (selectionFunc(i, j, k, item))
                        yield return Tuple.Create(i, j, k, mappingFunc(item));
                }
    }

    
    public static T[] MapItems<T>(this IReadOnlyList<T> array1, IReadOnlyList<T> array2, T initItem, Func<T, T, T> itemMapping)
    {
        var count1 = array1.Count;
        var count2 = array2.Count;
        var count = Math.Max(count1, count2);

        var newArray = new T[count];

        for (var i = 0; i < count; i++)
        {
            var s1 = i < count1
                ? array1[i]
                : initItem;

            var s2 = i < count2
                ? array2[i]
                : initItem;

            newArray[i] = itemMapping(s1, s2);
        }

        return newArray;
    }

    public static TOut[] MapItems<TIn, TOut>(this IReadOnlyList<TIn> array1, Func<TIn, TOut> itemMapping)
    {
        var count = array1.Count;
        var newArray = new TOut[count];

        for (var i = 0; i < count; i++)
            newArray[i] = itemMapping(array1[i]);

        return newArray;
    }

    public static TOut[] MapItems<TIn, TOut>(this IReadOnlyList<TIn> array1, Func<int, TIn, TOut> itemMapping)
    {
        var count = array1.Count;
        var newArray = new TOut[count];

        for (var i = 0; i < count; i++)
            newArray[i] = itemMapping(i, array1[i]);

        return newArray;
    }

    public static TOut[] MapItems<TIn1, TIn2, TOut>(this IReadOnlyList<TIn1> array1, IReadOnlyList<TIn2> array2, Func<TIn1> initFunc1, Func<TIn2> initFunc2, Func<TIn1, TIn2, TOut> itemMapping)
    {
        var count1 = array1.Count;
        var count2 = array2.Count;
        var count = Math.Max(count1, count2);

        var newArray = new TOut[count];

        for (var i = 0; i < count; i++)
        {
            var s1 = i < count1
                ? array1[i]
                : initFunc1();

            var s2 = i < count2
                ? array2[i]
                : initFunc2();

            newArray[i] = itemMapping(s1, s2);
        }

        return newArray;
    }


    public static T[,] MapItems<T>(this T[,] array, Func<T, T> itemMapping, T initItem, Func<T, bool> itemFilter)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        var newArray = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < colsCount; j++)
        {
            var item = array[i, j];

            newArray[i, j] = itemFilter(item) 
                ? itemMapping(array[i, j]) 
                : initItem;
        }

        return newArray;
    }
    
    public static T[,] MapItems<T>(this T[,] array1, T[,] array2, T initItem, Func<T, T, T, T> accumulatorFunc)
    {
        var rowsCount1 = array1.GetLength(0);
        var colsCount1 = array1.GetLength(1);

        var rowsCount2 = array2.GetLength(0);
        var colsCount2 = array2.GetLength(1);

        var innerCount = Math.Max(colsCount1, rowsCount2);

        var newArray = new T[rowsCount1, colsCount2];

        for (var i = 0; i < rowsCount1; i++)
        {
            for (var j = 0; j < colsCount2; j++)
            {
                var item = initItem;

                for (var k = 0; k < innerCount; k++)
                {
                    var aik = i < rowsCount1 && k < colsCount1
                        ? array1[i, k]
                        : initItem;

                    var bkj = k < rowsCount2 && j < colsCount2
                        ? array2[k, j]
                        : initItem;

                    item = accumulatorFunc(item, aik, bkj);
                }

                newArray[i, j] = item;
            }
        }

        return newArray;
    }

    public static T[,] MapItems<T>(this T[,] array1, T item2, Func<T, T, T> itemMapping)
    {
        var rowsCount = array1.GetLength(0);
        var colsCount = array1.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < colsCount; j++)
        {
            array[i, j] = itemMapping(
                array1[i, j],
                item2
            );
        }

        return array;
    }

    public static T[,] MapItems<T>(this T item1, T[,] array2, Func<T, T, T> itemMapping)
    {
        var rowsCount = array2.GetLength(0);
        var colsCount = array2.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < colsCount; j++)
        {
            array[i, j] = itemMapping(
                item1,
                array2[i, j]
            );
        }

        return array;
    }

    public static T[,] MapItems<T>(this T[,] array1, T[,] array2, Func<T, T, T> itemMapping)
    {
        var rowsCount = array1.GetLength(0);
        var colsCount = array1.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
        for (var j = 0; j < colsCount; j++)
        {
            array[i, j] = itemMapping(
                array1[i, j],
                array2[i, j]
            );
        }

        return array;
    }
    
    public static T[,] MapItems<T>(this T[,] array1, T[,] array2, T initItem, Func<T, T, T> itemMapping)
    {
        var rowCount1 = array1.GetLength(0);
        var colCount1 = array1.GetLength(1);

        var rowCount2 = array2.GetLength(0);
        var colCount2 = array1.GetLength(2);

        var rowCount = Math.Max(rowCount1, rowCount2);
        var colCount = Math.Max(colCount1, colCount2);

        var newArray = new T[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
        for (var i = 0; i < rowCount; i++)
        {
            var s1 = i < rowCount1 && j < colCount1
                ? array1[i, j]
                : initItem;

            var s2 = i < rowCount2 && j < colCount2
                ? array2[i, j]
                : initItem;

            newArray[i, j] = itemMapping(s1, s2);
        }

        return newArray;
    }


    public static TOut[] MapItems<TIn, TOut>(this TIn[] arrayIn, Func<TIn, TOut> mapFunc)
    {
        var n = arrayIn.Length;
        var arrayOut = new TOut[n];

        for (var i = 0; i < n; i++)
            arrayOut[i] = mapFunc(arrayIn[i]);

        return arrayOut;
    }

    public static TOut[,] MapItems<TIn, TOut>(this TIn[,] arrayIn, Func<TIn, TOut> mapFunc)
    {
        var n1 = arrayIn.GetLength(0);
        var n2 = arrayIn.GetLength(1);
        var arrayOut = new TOut[n1, n2];

        for (var i = 0; i < n1; i++)
        for (var j = 0; j < n2; j++)
            arrayOut[i, j] = mapFunc(arrayIn[i, j]);

        return arrayOut;
    }

    public static TOut[,,] MapItems<TIn, TOut>(this TIn[,,] arrayIn, Func<TIn, TOut> mapFunc)
    {
        var n1 = arrayIn.GetLength(0);
        var n2 = arrayIn.GetLength(1);
        var n3 = arrayIn.GetLength(2);
        var arrayOut = new TOut[n1, n2, n3];

        for (var i = 0; i < n1; i++)
        for (var j = 0; j < n2; j++)
        for (var k = 0; k < n3; k++)
            arrayOut[i, j, k] = mapFunc(arrayIn[i, j, k]);

        return arrayOut;
    }
    
    public static TOut[,] MapItems<TIn, TOut>(this TIn[,] array1, Func<int, int, TIn, TOut> itemMapping)
    {
        var rowsCount = array1.GetLength(0);
        var colsCount = array1.GetLength(1);
        var newArray = new TOut[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newArray[i, j] = itemMapping(i, j, array1[i, j]);

        return newArray;
    }

    public static TOut[,] MapItems<TIn1, TIn2, TOut>(this TIn1[,] array1, TIn2[,] array2, Func<TIn1> initFunc1, Func<TIn2> initFunc2, Func<TIn1, TIn2, TOut> itemMapping)
    {
        var rowCount1 = array1.GetLength(0);
        var colCount1 = array1.GetLength(1);

        var rowCount2 = array2.GetLength(0);
        var colCount2 = array1.GetLength(2);

        var rowCount = Math.Max(rowCount1, rowCount2);
        var colCount = Math.Max(colCount1, colCount2);

        var newArray = new TOut[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
            for (var i = 0; i < rowCount; i++)
            {
                var s1 = i < rowCount1 && j < colCount1
                    ? array1[i, j]
                    : initFunc1();

                var s2 = i < rowCount2 && j < colCount2
                    ? array2[i, j]
                    : initFunc2();

                newArray[i, j] = itemMapping(s1, s2);
            }

        return newArray;
    }

    //public static T[,] MapItems(this T[,] array1, T[,] array2, T initItem, Func<T, T, T> accumulatorFunc)
    //{
    //    var rowsCount1 = array1.GetLength(0);
    //    var colsCount1 = array1.GetLength(1);

    //    var rowsCount2 = array2.GetLength(0);
    //    var colsCount2 = array2.GetLength(1);

    //    var innerCount = Math.Max(colsCount1, rowsCount2);

    //    var newArray = new T[rowsCount1, colsCount2];

    //    for (var i = 0; i < rowsCount1; i++)
    //    {
    //        for (var j = 0; j < colsCount2; j++)
    //        {
    //            var item = initItem;

    //            for (var k = 0; k < innerCount; k++)
    //            {
    //                var aik = i < rowsCount1 && k < colsCount1
    //                    ? array1[i, k]
    //                    : initItem;

    //                var bkj = k < rowsCount2 && j < colsCount2
    //                    ? array2[k, j]
    //                    : initItem;

    //                item = accumulatorFunc(item, aik, bkj);
    //            }

    //            newArray[i, j] = item;
    //        }
    //    }

    //    return newArray;
    //}

    public static TOut[,] MapItems<TIn1, TIn2, TOut>(this TIn1[,] array1, TIn2[,] array2, Func<TIn1> initFunc1, Func<TIn2> initFunc2, Func<TOut> accumulatorInitFunc, Func<TOut, TIn1, TIn2, TOut> accumulatorFunc)
    {
        var rowsCount1 = array1.GetLength(0);
        var colsCount1 = array1.GetLength(1);

        var rowsCount2 = array2.GetLength(0);
        var colsCount2 = array2.GetLength(1);

        var innerCount = Math.Max(colsCount1, rowsCount2);

        var newArray = new TOut[rowsCount1, colsCount2];

        for (var i = 0; i < rowsCount1; i++)
        {
            for (var j = 0; j < colsCount2; j++)
            {
                var accumulator = accumulatorInitFunc();

                for (var k = 0; k < innerCount; k++)
                {
                    var aik = i < rowsCount1 && k < colsCount1
                        ? array1[i, k]
                        : initFunc1();

                    var bkj = k < rowsCount2 && j < colsCount2
                        ? array2[k, j]
                        : initFunc2();

                    accumulator = accumulatorFunc(accumulator, aik, bkj);
                }

                newArray[i, j] = accumulator;
            }
        }

        return newArray;
    }
    

    public static T[,] GetArrayCopy<T>(this T[,] array)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var arrayOut = new T[n2, n1];

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                arrayOut[i, j] = array[i, j];

        return arrayOut;
    }

    public static T[,] GetSubArray<T>(this T[,] array, int row1, int col1, int rowCount, int colCount)
    {
        var arrayOut = new T[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
            for (var j = 0; j < colCount; j++)
                arrayOut[i, j] = array[i + row1, j + col1];

        return arrayOut;
    }

    public static double[,] Add(this double[,] array1, double[,] array2)
    {
        return array1.MapItems(array2, (a, b) => a + b);
    }

    public static double[,] Subtract(this double[,] array1, double[,] array2)
    {
        return array1.MapItems(array2, (a, b) => a - b);
    }

    public static T[,] Transpose<T>(this T[,] array)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var arrayOut = new T[n2, n1];

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                arrayOut[j, i] = array[i, j];

        return arrayOut;
    }


    public static void SetItems<T>(this T[] array, Func<T, T> mapFunc)
    {
        var n1 = array.Length;

        for (var i = 0; i < n1; i++)
            array[i] = mapFunc(array[i]);
    }

    public static void SetItems<T>(this T[,] array, Func<T, T> mapFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                array[i, j] = mapFunc(array[i, j]);
    }

    public static void SetItems<T>(this T[,,] array, Func<T, T> mapFunc)
    {
        var n1 = array.GetLength(0);
        var n2 = array.GetLength(1);
        var n3 = array.GetLength(2);

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                    array[i, j, k] = mapFunc(array[i, j, k]);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] GetShallowCopy<T>(this T[] arrayIn)
    {
        var arrayOut = new T[arrayIn.Length];

        arrayIn.CopyTo(arrayOut, 0);

        return arrayOut;
    }

    public static T[] GetShallowCopy<T>(this T[] arrayIn, int i1, int i2)
    {
        var n1 = i2 - i1 + 1;
        var arrayOut = new T[n1];

        for (var i = 0; i < n1; i++)
            arrayOut[i] = arrayIn[i + i1];

        return arrayOut;
    }

    public static T[] GetShallowCopy<T>(this T[] arrayIn, IReadOnlyList<int> indicesList)
    {
        var n1 = indicesList.Count;
        var arrayOut = new T[n1];

        for (var i = 0; i < n1; i++)
        {
            var rowIndex = indicesList[i];

            arrayOut[i] = arrayIn[rowIndex];
        }

        return arrayOut;
    }

    public static T[,] GetShallowCopy<T>(this T[,] arrayIn, int i1, int i2, int j1, int j2)
    {
        var n1 = i2 - i1 + 1;
        var n2 = j2 - j1 + 1;
        var arrayOut = new T[n1, n2];

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                arrayOut[i, j] = arrayIn[i + i1, j + j1];

        return arrayOut;
    }

    public static T[,] GetShallowCopy<T>(this T[,] arrayIn, IReadOnlyList<int> rowIndicesList, IReadOnlyList<int> colIndicesList)
    {
        var n1 = rowIndicesList.Count;
        var n2 = colIndicesList.Count;
        var arrayOut = new T[n1, n2];

        for (var i = 0; i < n1; i++)
        {
            var rowIndex = rowIndicesList[i];

            for (var j = 0; j < n2; j++)
            {
                var colIndex = colIndicesList[j];

                arrayOut[i, j] = arrayIn[rowIndex, colIndex];
            }
        }

        return arrayOut;
    }

    public static T[,,] GetShallowCopy<T>(this T[,,] arrayIn, int i1, int i2, int j1, int j2, int k1, int k2)
    {
        var n1 = i2 - i1 + 1;
        var n2 = j2 - j1 + 1;
        var n3 = k2 - k1 + 1;
        var arrayOut = new T[n1, n2, n3];

        for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                for (var k = 0; k < n3; k++)
                    arrayOut[i, j, k] = arrayIn[i + i1, j + j1, k + k1];

        return arrayOut;
    }

    //TODO: Complete this: GetRow, SetRow, GetColumn, SetColumn, SwapRows, SwapColumns, etc.



}