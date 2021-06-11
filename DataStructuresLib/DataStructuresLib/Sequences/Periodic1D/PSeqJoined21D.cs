using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public class PSeqJoined21D<T>
        : IPeriodicSequencesAggregate1D<T>
    {
        public IPeriodicSequence1D<T> BaseSequence1 { get; }

        public IPeriodicSequence1D<T> BaseSequence2 { get; }

        public IReadOnlyList<IPeriodicSequence1D<T>> BaseSequences 
            => new []
            {
                BaseSequence1, 
                BaseSequence2
            };


        public int Count
            => BaseSequence1.Count + BaseSequence2.Count;

        public T this[int index] 
        {
            get
            {
                index = index.Mod(Count);

                if (index < BaseSequence1.Count)
                    return BaseSequence1[index];

                index -= BaseSequence1.Count;

                return BaseSequence2[index];
            }
        }

        public bool IsBasic 
            => false;

        public bool IsOperator 
            => true;


        internal PSeqJoined21D(IPeriodicSequence1D<T> baseSequence1, IPeriodicSequence1D<T> baseSequence2)
        {
            BaseSequence1 = baseSequence1;
            BaseSequence2 = baseSequence2;
        }

        
        public IEnumerator<T> GetEnumerator()
        {
            return BaseSequences.SelectMany(s => s).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return BaseSequences.SelectMany(s => s).GetEnumerator();
        }
    }
}