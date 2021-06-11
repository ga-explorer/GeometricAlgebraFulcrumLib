using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Collections.PeriodicLists
{
    public class ProListConstantValues<TValue> :
        IPeriodicReadOnlyList<TValue> 
    {
        public TValue Value { get; }

        public int Count { get; }

        public TValue this[int index] 
            => Value;


        public ProListConstantValues(int count, TValue value)
        {
            Count = count;
            Value = value;
        }

        
        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable.Repeat(Value, Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}