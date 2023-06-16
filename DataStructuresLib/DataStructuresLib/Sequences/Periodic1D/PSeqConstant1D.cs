using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public class PSeqConstant1D<T>
        : IPeriodicSequence1D<T>
    {
        public T Value { get; set; }

        public int Count { get; }

        public T this[int index] 
            => Value;

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PSeqConstant1D(int count)
        {
            if (count < 2)
                throw new ArgumentOutOfRangeException(nameof(count));

            Value = default;
            Count = count;
        }

        public PSeqConstant1D(int count, T value)
        {
            if (count < 2)
                throw new ArgumentOutOfRangeException(nameof(count));

            Value = value;
            Count = count;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Repeat(Value, Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.Repeat(Value, Count).GetEnumerator();
        }
    }
}