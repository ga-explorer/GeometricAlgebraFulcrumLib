using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists
{
    public class Array2DAsReadOnlyList<T> : 
        IReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Row(T[,] baseArray, int rowIndex)
        {
            var count = baseArray.GetLength(1);

            return new Array2DAsReadOnlyList<T>(
                baseArray,
                count,
                index => new Pair<int>(rowIndex, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Column(T[,] baseArray, int colIndex)
        {
            var count = baseArray.GetLength(0);

            return new Array2DAsReadOnlyList<T>(
                baseArray,
                count,
                index => new Pair<int>(index, colIndex)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Diagonal(T[,] baseArray)
        {
            var count = Math.Min(
                baseArray.GetLength(0), 
                baseArray.GetLength(1)
            );

            return new Array2DAsReadOnlyList<T>(
                baseArray,
                count,
                index => new Pair<int>(index, index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> SubDiagonal(T[,] baseArray, int offset)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);

            // Compute starting position of diagonal
            var startRow = offset >= 0 ? offset : 0;
            var startCol = offset >= 0 ? 0 : -offset;

            // Check if start is out of bounds
            if (startRow >= rows || startCol >= cols)
                return new Array2DAsReadOnlyList<T>(
                    baseArray, 
                    0, 
                    _ => new Pair<int>(0, 0)
                );

            // Number of elements on this diagonal
            var count = Math.Min(rows - startRow, cols - startCol);

            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                index => new Pair<int>(startRow + index, startCol + index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> FullRowMajor(T[,] baseArray)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);
            var count = rows * cols;

            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                IndexToIndexPairFunc
            );

            Pair<int> IndexToIndexPairFunc(int index)
            {
                var i = index / cols; // row index
                var j = index % cols; // column index
                return new Pair<int>(i, j);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> FullColumnMajor(T[,] baseArray)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);
            var count = rows * cols;

            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                IndexToIndexPairFunc
            );

            Pair<int> IndexToIndexPairFunc(int index)
            {
                var j = index / rows; // column index
                var i = index % rows; // row index
                return new Pair<int>(i, j);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Create(T[,] baseArray, params (int, int)[] indexPairs)
        {
            var count = indexPairs.Length;

            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                index => indexPairs[index]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Create(T[,] baseArray, IReadOnlyList<IPair<int>> indexPairList)
        {
            var count = indexPairList.Count;

            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                index => indexPairList[index].ToPair()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<T> Create(T[,] baseArray, int count, Func<int, Pair<int>> indexToIndexPairMap)
        {
            return new Array2DAsReadOnlyList<T>(
                baseArray, 
                count, 
                indexToIndexPairMap
            );
        }


        public T[,] BaseArray { get; }

        public int Count { get; }

        public Func<int, Pair<int>> IndexToIndexPairMap { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Array2DAsReadOnlyList(T[,] baseArray, int count, Func<int, Pair<int>> indexToIndexPairMap)
        {
            BaseArray = baseArray;
            Count = count;
            IndexToIndexPairMap = indexToIndexPairMap;
        }

        
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                var (i, j) = IndexToIndexPairMap(index);

                return BaseArray[i, j];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                var (i, j) = IndexToIndexPairMap(index);

                yield return BaseArray[i, j];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Array2DAsReadOnlyList<TIn, TOut> : 
        IReadOnlyList<TOut>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Row(TIn[,] baseArray, Func<TIn, TOut> itemMap, int rowIndex)
        {
            var count = baseArray.GetLength(1);

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray,
                itemMap,
                count,
                index => new Pair<int>(rowIndex, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Column(TIn[,] baseArray, Func<TIn, TOut> itemMap, int colIndex)
        {
            var count = baseArray.GetLength(0);

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray,
                itemMap,
                count,
                index => new Pair<int>(index, colIndex)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Diagonal(TIn[,] baseArray, Func<TIn, TOut> itemMap)
        {
            var count = Math.Min(
                baseArray.GetLength(0), 
                baseArray.GetLength(1)
            );

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray,
                itemMap,
                count,
                index => new Pair<int>(index, index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> SubDiagonal(TIn[,] baseArray, Func<TIn, TOut> itemMap, int offset)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);

            // Compute starting position of diagonal
            var startRow = offset >= 0 ? offset : 0;
            var startCol = offset >= 0 ? 0 : -offset;

            // Check if start is out of bounds
            if (startRow >= rows || startCol >= cols)
                return new Array2DAsReadOnlyList<TIn, TOut>(
                    baseArray, 
                    itemMap,
                    0, 
                    _ => new Pair<int>(0, 0)
                );

            // Number of elements on this diagonal
            var count = Math.Min(rows - startRow, cols - startCol);

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                index => new Pair<int>(startRow + index, startCol + index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> FullRowMajor(TIn[,] baseArray, Func<TIn, TOut> itemMap)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);
            var count = rows * cols;

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                IndexToIndexPairFunc
            );

            Pair<int> IndexToIndexPairFunc(int index)
            {
                var i = index / cols; // row index
                var j = index % cols; // column index
                return new Pair<int>(i, j);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> FullColumnMajor(TIn[,] baseArray, Func<TIn, TOut> itemMap)
        {
            var rows = baseArray.GetLength(0);
            var cols = baseArray.GetLength(1);
            var count = rows * cols;

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                IndexToIndexPairFunc
            );

            Pair<int> IndexToIndexPairFunc(int index)
            {
                var j = index / rows; // column index
                var i = index % rows; // row index
                return new Pair<int>(i, j);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Create(TIn[,] baseArray, Func<TIn, TOut> itemMap, params (int, int)[] indexPairs)
        {
            var count = indexPairs.Length;

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                index => indexPairs[index]
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Create(TIn[,] baseArray, Func<TIn, TOut> itemMap, IReadOnlyList<IPair<int>> indexPairList)
        {
            var count = indexPairList.Count;

            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                index => indexPairList[index].ToPair()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Array2DAsReadOnlyList<TIn, TOut> Create(TIn[,] baseArray, Func<TIn, TOut> itemMap, int count, Func<int, Pair<int>> indexToIndexPairMap)
        {
            return new Array2DAsReadOnlyList<TIn, TOut>(
                baseArray, 
                itemMap,
                count, 
                indexToIndexPairMap
            );
        }


        public TIn[,] BaseArray { get; }

        public int Count { get; }

        public Func<int, Pair<int>> IndexToIndexPairMap { get; }

        public Func<TIn, TOut> ItemMap { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Array2DAsReadOnlyList(TIn[,] baseArray, Func<TIn, TOut> itemMap, int count, Func<int, Pair<int>> indexToIndexPairMap)
        {
            BaseArray = baseArray;
            Count = count;
            IndexToIndexPairMap = indexToIndexPairMap;
            ItemMap = itemMap;
        }

        
        public TOut this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                var (i, j) = IndexToIndexPairMap(index);

                return ItemMap(BaseArray[i, j]);
            }
        }

        public IEnumerator<TOut> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                var (i, j) = IndexToIndexPairMap(index);

                yield return ItemMap(BaseArray[i, j]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
