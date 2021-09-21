using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Extensions
{
    public static class ArrayExtensions
    {
        


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

        public static T[] ColumnToArray1D<T>(this T[,] arrayIn, int columnIndex)
        {
            var n1 = arrayIn.GetLength(0);

            var arrayOut = new T[n1];
            for (var i = 0; i < n1; i++)
                arrayOut[i] = arrayIn[i, columnIndex];

            return arrayOut;
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

        public static IEnumerable<T> GetRowItems<T>(this T[,] array, int rowIndex)
        {
            return Enumerable
                .Range(0, array.GetLength(1))
                .Select(i => array[rowIndex, i]);
        }

        public static IEnumerable<T> GetColumnItems<T>(this T[,] array, int columnIndex)
        {
            return Enumerable
                .Range(0, array.GetLength(0))
                .Select(i => array[i, columnIndex]);
        }


        public static IEnumerable<Tuple<int, T>> GetIndexItemTuples<T>(this T[] array)
        {
            var n1 = array.Length;

            for (var i = 0; i < n1; i++)
                yield return Tuple.Create(i, array[i]);
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


        public static IEnumerable<Tuple<int, int, T>> GetIndexItemTuples<T>(this T[,] array)
        {
            var n1 = array.GetLength(0);
            var n2 = array.GetLength(1);

            for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                yield return Tuple.Create(i, j, array[i, j]);
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


        public static T[] GetSubArray<T>(this T[] arrayIn, int i1, int i2)
        {
            var n1 = i2 - i1 + 1;
            var arrayOut = new T[n1];

            for (var i = 0; i < n1; i++)
                arrayOut[i] = arrayIn[i + i1];

            return arrayOut;
        }

        public static T[] GetSubArray<T>(this T[] arrayIn, IReadOnlyList<int> indicesList)
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

        public static T[,] GetSubArray<T>(this T[,] arrayIn, int i1, int i2, int j1, int j2)
        {
            var n1 = i2 - i1 + 1;
            var n2 = j2 - j1 + 1;
            var arrayOut = new T[n1, n2];

            for (var i = 0; i < n1; i++)
            for (var j = 0; j < n2; j++)
                arrayOut[i, j] = arrayIn[i + i1, j + j1];

            return arrayOut;
        }

        public static T[,] GetSubArray<T>(this T[,] arrayIn, IReadOnlyList<int> rowIndicesList, IReadOnlyList<int> colIndicesList)
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

        public static T[,,] GetSubArray<T>(this T[,,] arrayIn, int i1, int i2, int j1, int j2, int k1, int k2)
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
}
