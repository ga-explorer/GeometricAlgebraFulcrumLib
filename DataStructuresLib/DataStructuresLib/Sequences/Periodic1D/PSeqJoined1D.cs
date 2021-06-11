using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public class PSeqJoined1D<T>
        : IPeriodicSequencesAggregate1D<T>
    {
        public IReadOnlyList<IPeriodicSequence1D<T>> BaseSequences { get; }

        public int Count
            => BaseSequences.Sum(s => s.Count);

        public T this[int index] 
        {
            get
            {
                index = index.Mod(Count);

                foreach (var sequence in BaseSequences)
                {
                    if (index < sequence.Count)
                        return sequence[index];
                    
                    index -= sequence.Count;
                }

                throw new InvalidOperationException();
            }
        }

        public bool IsBasic 
            => false;

        public bool IsOperator 
            => true;


        public PSeqJoined1D(params IPeriodicSequence1D<T>[] sequencesArray)
        {
            BaseSequences = sequencesArray;
        }

        public PSeqJoined1D(IEnumerable<IPeriodicSequence1D<T>> sequencesList)
        {
            BaseSequences = sequencesList.ToArray();
        }

        public PSeqJoined1D(IReadOnlyList<IPeriodicSequence1D<T>> sequencesList)
        {
            BaseSequences = sequencesList;
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