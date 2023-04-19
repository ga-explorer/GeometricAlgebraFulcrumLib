using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.IndexSets
{
    public sealed record IndexRange : 
        IReadOnlyList<int>
    {
        public int Index1 { get; private set; }

        public int Index2 { get; private set; }

        public int Count { get; private set; }
    
        public int this[int index] 
            => index >= 0 && index < Count
                ? Index1 + index
                : throw new IndexOutOfRangeException();

        public bool IsValid 
            => Count > 0;

        public bool IsCountEven 
            => Count > 0 && (Count & 1) == 0;
    
        public bool IsCountOdd 
            => Count > 0 && (Count & 1) != 0;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexRange(int index2)
        {
            Index1 = 0;
            Index2 = index2;

            Count = Index2 - Index1 + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexRange(int index1, int index2)
        {
            Index1 = index1;
            Index2 = index2;

            Count = Index2 - Index1 + 1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetItems<T>(IReadOnlyList<T> itemList)
        {
            for (var i = Index1; i <= Index2; i++)
                yield return itemList[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseIndex1()
        {
            Index1++;
            Count = Index2 - Index1 + 1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseIndex2()
        {
            Index2++;
            Count = Index2 - Index1 + 1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DecreaseIndex1()
        {
            Index1--;
            Count = Index2 - Index1 + 1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DecreaseIndex2()
        {
            Index2--;
            Count = Index2 - Index1 + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<int> GetEnumerator()
        {
            return IsValid
                ? Enumerable.Range(Index1, Count).GetEnumerator()
                : Enumerable.Empty<int>().GetEnumerator();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var invalidText = IsValid ? string.Empty : " (Invalid)";

            return $"[{Index1}, {Index2}] : {Count}{invalidText}";
        }
    }
}