using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ReadOnlyLists
{
    public sealed record GaReadOnlyListEmpty<T>
        : IReadOnlyList<T>
    {
        public static GaReadOnlyListEmpty<T> DefaultList { get; }
            = new GaReadOnlyListEmpty<T>();


        public int Count => 0;

        public T this[int index] 
            => throw new IndexOutOfRangeException(nameof(index));


        private GaReadOnlyListEmpty()
        {
        }

        
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}