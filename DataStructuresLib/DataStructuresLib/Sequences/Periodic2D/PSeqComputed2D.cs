using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;

namespace DataStructuresLib.Sequences.Periodic2D
{
    public class PSeqComputed2D<T>
        : IPeriodicSequence2D<T>
    {
        public int Count1 { get; }

        public int Count2 { get; }

        public int Count 
            => Count1 * Count2;

        public Func<int, int, T> MappingFunction { get; }

        public T this[int index]
        {
            get
            {
                var (index1, index2) = this.GetItemIndexPair(index);

                return MappingFunction(index1, index2);
            }
        }

        public T this[int index1, int index2] 
            => MappingFunction(
                index1.Mod(Count), 
                index2.Mod(Count)
            );

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PSeqComputed2D(int count1, int count2, [NotNull] Func<int, int, T> mappingFunction)
        {
            Count1 = count1;
            Count2 = count2;
            MappingFunction = mappingFunction;
        }


        public PSeqSlice1D<T> GetSliceAt(int dimension, int index)
        {
            return new PSeqSlice1D<T>(this, dimension, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i2 = 0; i2 < Count2; i2++)
            for (var i1 = 0; i1 < Count1; i1++)
                yield return MappingFunction(i1, i2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}