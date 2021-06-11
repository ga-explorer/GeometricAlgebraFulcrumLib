using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Sequences.Periodic2D;

namespace DataStructuresLib.Sequences.Periodic1D
{
    /// <summary>
    /// This class is a periodic sequence created as a slice from a base
    /// 2D periodic sequence
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PSeqSlice1D<T>
        : IPeriodicSequence1D<T>
    {
        public IPeriodicSequence2D<T> BaseSequence { get; }

        public int SliceDimension { get; }

        public int SliceIndex { get; }

        public int Count 
            => (SliceDimension & 1) == 0
                ? BaseSequence.Count1
                : BaseSequence.Count2;

        public T this[int index]
            => (SliceDimension & 1) == 0
                ? BaseSequence[index, SliceIndex]
                : BaseSequence[SliceIndex, index];

        public bool IsBasic
            => true;

        public bool IsOperator
            => false;


        public PSeqSlice1D(IPeriodicSequence2D<T> baseSequence, int sliceDimension, int sliceIndex)
        {
            BaseSequence = baseSequence;
            SliceDimension = sliceDimension;
            SliceIndex = sliceIndex;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return (SliceDimension & 1) == 0 
                ? Enumerable
                    .Range(0, BaseSequence.Count1)
                    .Select(i => BaseSequence[i, SliceIndex])
                    .GetEnumerator()
                : Enumerable
                    .Range(0, BaseSequence.Count2)
                    .Select(i => BaseSequence[SliceIndex, i])
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
