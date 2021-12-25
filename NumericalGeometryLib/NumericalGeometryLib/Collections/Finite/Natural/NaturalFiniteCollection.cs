using System.Collections.Generic;

namespace NumericalGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents any collection that contains a number of elements
    /// where each element has a specific zero-based index in the collection. This includes
    /// 1-D arrays (vectors), Lists, Dictionaries with int keys, functions having a single
    /// int input, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NaturalFiniteCollection<T> : FiniteCollection<T>
    {
        public override int MinIndex
        {
            get { return 0; }
        }

        public override int MaxIndex
        {
            get { return Count - 1; }
        }


        public NfcChained<T> ToChainedCollection()
        {
            var chainedCol = this as NfcChained<T>;

            return chainedCol ?? NfcChained<T>.Create(this);
        }

        public NfcVector<T> ToVectorCollection()
        {
            var vectorCol = this as NfcVector<T>;

            return vectorCol ?? NfcVector<T>.CreateFromList(this);
        }

        public NfcRepeated<T> ToRepeatedCollection(int repeatCount)
        {
            var repeatedCol = this as NfcRepeated<T>;

            if (repeatedCol != null)
            {
                repeatedCol.Reset(
                    repeatCount * repeatedCol.RepeatCount, 
                    repeatedCol.BaseCollection
                    );

                return repeatedCol;
            }

            return NfcRepeated<T>.Create(repeatCount, this);
        }

        public NfcList<T> ToListCollection()
        {
            var listCol = this as NfcList<T>;

            return listCol ?? NfcList<T>.CreateFromList(this);
        }

        public NfcSlice<T> SliceCollection(int firstIndex, int lastIndex)
        {
            return NfcSlice<T>.Create(this, firstIndex, lastIndex);
        }

        public NaturalFiniteCollection<T> ReverseCollection()
        {
            var revCol = this as NfcReversed<T>;

            return (revCol == null) 
                ? NfcReversed<T>.Create(this) 
                : revCol.BaseCollection.ToNaturalCollection();
        }


        public NfcChained<T> AppendCollections(params FiniteCollection<T>[] colList)
        {
            var chainedCol = this as NfcChained<T>;

            if (chainedCol == null)
            {
                var result = NfcChained<T>.Create();

                result.BaseCollections.Add(this);
                result.BaseCollections.AddRange(colList);

                return result;
            }

            chainedCol.BaseCollections.AddRange(colList);

            return chainedCol;
        }

        public NfcChained<T> AppendCollections(IEnumerable<NaturalFiniteCollection<T>> colList)
        {
            var chainedCol = this as NfcChained<T>;

            if (chainedCol == null)
            {
                var result = NfcChained<T>.Create();

                result.BaseCollections.Add(this);
                result.BaseCollections.AddRange(colList);

                return result;
            }

            chainedCol.BaseCollections.AddRange(colList);

            return chainedCol;
        }
    }
}
