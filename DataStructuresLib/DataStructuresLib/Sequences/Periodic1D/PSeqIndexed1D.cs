using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public abstract class PSeqIndexed1D<T>
        : IPeriodicSequence1D<T>
    {
        public int Count { get; }

        public T this[int index]
            => MappingFunction(index.Mod(Count));

        public bool IsBasic
            => true;

        public bool IsOperator
            => false;


        protected PSeqIndexed1D(int count)
        {
            Count = count;
        }


        public abstract T MappingFunction(int index);

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(MappingFunction)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(MappingFunction)
                .GetEnumerator();
        }
    }
}