using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Collections.PeriodicLists
{
    public class ProListComputedValues<TValue> :
        IPeriodicReadOnlyList<TValue>
    {
        public Func<int, TValue> ComputingFunc { get; }

        public int Count { get; }

        public TValue this[int index] 
            => ComputingFunc(index.Mod(Count));
        

        public ProListComputedValues(int count, Func<int, TValue> computingFunc)
        {
            Count = count;
            ComputingFunc = computingFunc;
        }


        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(ComputingFunc)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}