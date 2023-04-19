using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists
{
    public sealed class GaFuLListDense<T> :
        IList<T>, 
        IGaFuLReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> Create()
        {
            return new GaFuLListDense<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> Create(int capacity)
        {
            return new GaFuLListDense<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> Create(IEnumerable<T> itemsList)
        {
            return new GaFuLListDense<T>(itemsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> Create(List<T> itemsList)
        {
            return new GaFuLListDense<T>(itemsList);
        }


        private readonly List<T> _itemsList;

        
        public int SparseCount 
            => _itemsList.Count;

        public int Count 
            => _itemsList.Count;

        public bool IsReadOnly 
            => false;

        public T this[int index]
        {
            get => _itemsList[index];
            set => _itemsList[index] = value;
        }

        public T this[ulong index]
        {
            get => _itemsList[(int) index];
            set => _itemsList[(int) index] = value;
        }
        

        private GaFuLListDense()
        {
            _itemsList = new List<T>();
        }
        
        private GaFuLListDense(int capacity)
        {
            _itemsList = new List<T>(capacity);
        }
        
        private GaFuLListDense(IEnumerable<T> itemsList)
        {
            _itemsList = new List<T>(itemsList);
        }
        
        private GaFuLListDense(List<T> itemsList)
        {
            _itemsList = itemsList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            _itemsList.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _itemsList.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return _itemsList.Contains(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
        {
            _itemsList.CopyTo(array, arrayIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item)
        {
            return _itemsList.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
        {
            return _itemsList.IndexOf(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, T item)
        {
            _itemsList.Insert(index, item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index)
        {
            _itemsList.RemoveAt(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _itemsList.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return ((ulong) Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _itemsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return index < (ulong) Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong index, out T value)
        {
            if (index < (ulong) Count)
            {
                value = _itemsList[(int) index];
                return true;
            }
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs()
        {
            return ((ulong) Count).GetRange().Select(index => 
                new KeyValuePair<ulong, T>(index, _itemsList[(int) index])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}