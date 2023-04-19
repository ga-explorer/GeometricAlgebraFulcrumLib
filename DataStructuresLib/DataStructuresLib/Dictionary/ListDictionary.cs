using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace DataStructuresLib.Dictionary
{
    public sealed class ListDictionary<TValue> :
        IReadOnlyDictionary<int, TValue>
    {
        public IReadOnlyList<TValue> BaseList { get; }

        public int Count 
            => BaseList.Count;
    
        public TValue this[int key] 
            => BaseList[key];

        public IEnumerable<int> Keys 
            => BaseList.Count.GetRange();

        public IEnumerable<TValue> Values 
            => BaseList;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ListDictionary([NotNull] IReadOnlyList<TValue> baseList)
        {
            BaseList = baseList;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(int key)
        {
            return key >= 0 && key < BaseList.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(int key, out TValue value)
        {
            if (key >= 0 && key < BaseList.Count)
            {
                value = BaseList[key];
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
        {
            return BaseList.Select((value, index) => 
                new KeyValuePair<int, TValue>(index, value)
            ).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}