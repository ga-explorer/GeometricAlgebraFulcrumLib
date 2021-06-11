using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.Collections.Finite;
using EuclideanGeometryLib.Collections.Finite.Iterators;
using EuclideanGeometryLib.Collections.Finite.Natural;

namespace EuclideanGeometryLib.Collections
{
    public abstract class GenerativeCollection<T> : IGenerativeCollection<T>
    {
        public T DefaultValue { get; set; }


        public abstract T GetItem(int index);

        public virtual T[] GetItems(params int[] indexList)
        {
            var items = new T[indexList.Length];

            for (var i = 0; i < indexList.Length; i++)
                items[i] = GetItem(indexList[i]);

            return items;
        }

        public virtual IEnumerable<T> GetItems(IEnumerable<int> indexList)
        {
            return indexList.Select(GetItem);
        }

        public FiniteCollection<T> ToFiniteCollection(int firstIndex, int lastIndex)
        {
            var finiteCol = this as FiniteCollection<T>;

            if (finiteCol == null)
                return FcSlice<T>.Create(this, firstIndex, lastIndex);

            if (finiteCol.MinIndex == firstIndex && finiteCol.MaxIndex == lastIndex)
                return finiteCol;

            if (finiteCol.MaxIndex == firstIndex && finiteCol.MinIndex == lastIndex)
                return finiteCol;

            return FcSlice<T>.Create(this, firstIndex, lastIndex);
        }

        public FiniteCollection<T> ToFiniteCollection(T defaultValue, int firstIndex, int lastIndex)
        {
            var sliceCol = FcSlice<T>.Create(this, firstIndex, lastIndex);

            sliceCol.DefaultValue = defaultValue;

            return sliceCol;
        }

        public GcEven<T> ToEvenCollection()
        {
            return this as GcEven<T> ?? GcEven<T>.Create(this);
        }

        public GcEven<T> ToEvenCollection(int startIndex)
        {
            return GcEven<T>.Create(this, startIndex);
        }

        public GcPeriodic<T> ToPeriodicCollection(int firstIndex, int lastIndex)
        {
            return GcPeriodic<T>.Create(this, firstIndex, lastIndex);
        }

        /// <summary>
        /// Perform an index-shift operation on the elements of this collection
        /// </summary>
        /// <param name="shiftCount"></param>
        /// <returns></returns>
        public GcShiftReflect<T> ShiftBy(int shiftCount)
        {
            var srCol = this as GcShiftReflect<T> ?? GcShiftReflect<T>.Create(this);

            return srCol.ApplyShift(shiftCount);
        }

        /// <summary>
        /// Perform an index-reflection operation on the elements of this collection
        /// </summary>
        /// <param name="reflectionCenter"></param>
        /// <returns></returns>
        public GcShiftReflect<T> ReflectOn(int reflectionCenter)
        {
            var srCol = this as GcShiftReflect<T>
                ?? GcShiftReflect<T>.Create(this);

            return srCol.ApplyReflect(reflectionCenter);
        }

        /// <summary>
        /// Convert this collection into a natural collection
        /// </summary>
        /// <returns></returns>
        public NaturalFiniteCollection<T> ToNaturalCollection(int firstIndex, int lastIndex)
        {
            return NfcSlice<T>.Create(this, firstIndex, lastIndex);
        }


        public FciSliceIterator<T> GetSliceIterator(int firstIndex, int lastIndex)
        {
            return new FciSliceIterator<T>(this, firstIndex, lastIndex);
        }

        public FciPeriodicIterator<T> GetPeriodicIterator(int firstIndex, int lastIndex)
        {
            return new FciPeriodicIterator<T>(this, firstIndex, lastIndex);
        }

        public FciSwingIterator<T> GetSwingIterator(int firstIndex, int lastIndex)
        {
            return new FciSwingIterator<T>(this, firstIndex, lastIndex);
        }

        public FciMappedOffsetIterator<T> GetRandomPermutationIterator(int firstIndex, int lastIndex)
        {
            var reverseDirection = lastIndex < firstIndex;
            var count = (reverseDirection 
                ? firstIndex - lastIndex + 1 
                : lastIndex - firstIndex + 1);
            var offsetSequence = count.GetRandomPermutation();

            return new FciMappedOffsetIterator<T>(this, offsetSequence, firstIndex, reverseDirection);
        }

        public FciMappedOffsetIterator<T> GetRandomPermutationIterator(int firstIndex, int lastIndex, int seed)
        {
            var reverseDirection = lastIndex < firstIndex;
            var count = (reverseDirection
                ? firstIndex - lastIndex + 1
                : lastIndex - firstIndex + 1);
            var offsetSequence = count.GetRandomPermutation(seed);

            return new FciMappedOffsetIterator<T>(this, offsetSequence, firstIndex, reverseDirection);
        }

        public FciMappedIndexIterator<T> GetMappedIndexIterator(IEnumerable<int> indexSequence)
        {
            return new FciMappedIndexIterator<T>(this, indexSequence);
        }

        public FciMappedOffsetIterator<T> GetMappedOffsetIterator(IEnumerable<int> indexSequence, int firstIndex)
        {
            return new FciMappedOffsetIterator<T>(this, indexSequence, firstIndex, false);
        }

        public FciMappedOffsetIterator<T> GetMappedOffsetIterator(IEnumerable<int> indexSequence, int firstIndex, bool goForward)
        {
            return new FciMappedOffsetIterator<T>(this, indexSequence, firstIndex, !goForward);
        }
    }
}
