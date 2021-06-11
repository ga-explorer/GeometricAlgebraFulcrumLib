using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;

namespace DataStructuresLib.Sequences.Periodic2D
{
    public class PSeqTransposed2D<T>
        : IPeriodicSequence2D<T>
    {
        public IPeriodicSequence2D<T> BaseSequence { get; }

        public int Count1 
            => BaseSequence.Count2;

        public int Count2 
            => BaseSequence.Count1;

        public int Count 
            => BaseSequence.Count;

        public T this[int index]
        {
            get
            {
                var indexPair = this.GetItemIndexPair(index);

                return BaseSequence[indexPair.Item2, indexPair.Item1];
            }
        }

        public T this[int index1, int index2] 
            => BaseSequence[index2, index1];

        public bool IsBasic 
            => false;

        public bool IsOperator 
            => true;


        public PSeqTransposed2D(IPeriodicSequence2D<T> baseSequence)
        {
            BaseSequence = baseSequence;
        }


        public PSeqSlice1D<T> GetSliceAt(int dimension, int index)
        {
            return BaseSequence.GetSliceAt(dimension ^ 1, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i2 = 0; i2 < Count2; i2++)
            for (var i1 = 0; i1 < Count1; i1++)
                yield return BaseSequence[i2, i1];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}