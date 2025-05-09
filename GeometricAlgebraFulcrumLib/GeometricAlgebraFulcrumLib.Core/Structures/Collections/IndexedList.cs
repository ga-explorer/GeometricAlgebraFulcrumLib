using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections
{
    /// <summary>
    /// All operations on this class only change the index list, not the base list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IndexedList<T> :
        IReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexedList<T> Create(IReadOnlyList<T> baseList)
        {
            return new IndexedList<T>(
                baseList.Count.GetRange().ToArray(),
                baseList
            );
        }


        private int[] _indexArray;


        public IReadOnlyList<int> IndexList 
            => _indexArray;

        public IReadOnlyList<T> BaseList { get; }

        public int Count 
            => _indexArray.Length;

        public T this[int index] 
            => BaseList[_indexArray[index]];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IndexedList(int[] indexArray, IReadOnlyList<T> baseList)
        {
            if (indexArray.Length > baseList.Count)
                throw new InvalidOperationException();

            _indexArray = indexArray;
            BaseList = baseList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            var n = BaseList.Count;

            return _indexArray.Length <= n &&
                   _indexArray.All(i => i >= 0 && i < n);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValidIndexList(IReadOnlyList<int> indexList)
        {
            var n = BaseList.Count;

            return indexList.Count <= n &&
                   indexList.All(i => i >= 0 && i < n);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SwapItems(int index1, int index2)
        {
            (_indexArray[index1], _indexArray[index2]) = (_indexArray[index2], _indexArray[index1]);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SetIndexList(IReadOnlyList<int> indexList, bool assumeValid = true)
        {
            if (!assumeValid && !IsValidIndexList(indexList))
                throw new InvalidOperationException();

            Debug.Assert(!assumeValid || IsValidIndexList(indexList));

            _indexArray = indexList.ToArray();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> IndexFullBaseList()
        {
            _indexArray = BaseList.Count.GetRange().ToArray();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> IndexFullBaseListReverse()
        {
            var n = BaseList.Count - 1;

            _indexArray = (n + 1).MapRange(i => n - i).ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortIndexListAscending()
        {
            _indexArray = _indexArray.OrderBy(i => i).ToArray();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortIndexListDescending()
        {
            _indexArray = _indexArray.OrderBy(i => -i).ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> Shuffle(System.Random? random = null)
        {
            _indexArray.Shuffle(random);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsAscending()
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];

                        return new KeyValuePair<int, T>(
                            baseIndex, 
                            BaseList[baseIndex]
                        );
                    }
                ).OrderBy(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsAscending<T1>(Func<T, T1> indexValueMap)
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];
                        var baseValue = indexValueMap(BaseList[baseIndex]);

                        return new KeyValuePair<int, T1>(
                            baseIndex, 
                            baseValue
                        );
                    }
                ).OrderBy(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsAscending<T1>(Func<int, T, T1> indexValueMap)
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];
                        var baseValue = indexValueMap(i, BaseList[baseIndex]);

                        return new KeyValuePair<int, T1>(
                            baseIndex, 
                            baseValue
                        );
                    }
                ).OrderBy(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsDescending()
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];

                        return new KeyValuePair<int, T>(
                            baseIndex, 
                            BaseList[baseIndex]
                        );
                    }
                ).OrderByDescending(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsDescending<T1>(Func<T, T1> indexValueMap)
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];
                        var baseValue = indexValueMap(BaseList[baseIndex]);

                        return new KeyValuePair<int, T1>(
                            baseIndex, 
                            baseValue
                        );
                    }
                ).OrderByDescending(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexedList<T> SortItemsDescending<T1>(Func<int, T, T1> indexValueMap)
        {
            _indexArray =
                Count.MapRange(i =>
                    {
                        var baseIndex = _indexArray[i];
                        var baseValue = indexValueMap(i, BaseList[baseIndex]);

                        return new KeyValuePair<int, T1>(
                            baseIndex, 
                            baseValue
                        );
                    }
                ).OrderByDescending(p => p.Value)
                .Select(p => p.Key)
                .ToArray();

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            return _indexArray
                .Length
                .MapRange(i => BaseList[_indexArray[i]])
                .GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }
}
