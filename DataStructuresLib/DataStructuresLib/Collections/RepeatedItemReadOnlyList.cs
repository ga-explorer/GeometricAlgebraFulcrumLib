using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Collections
{
    public sealed class RepeatedItemReadOnlyList<T> :
        IReadOnlyList<T>
    {
        public T ItemValue { get; }

        public int Count { get; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return ItemValue;
            }
        }


        public RepeatedItemReadOnlyList(T itemValue, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            ItemValue = itemValue;
            Count = count;
        }

        
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Repeat(ItemValue, Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}