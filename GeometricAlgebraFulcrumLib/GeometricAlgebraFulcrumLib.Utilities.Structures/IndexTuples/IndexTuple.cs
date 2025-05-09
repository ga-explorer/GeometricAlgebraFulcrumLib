using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexTuples
{
    public readonly struct IndexTuple :
        IReadOnlyList<int>,
        IEquatable<IndexTuple>,
        IComparable<IndexTuple>
    {

        public static IndexTuple EmptyTuple { get; }
            = new IndexTuple([]);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(int index)
        {
            return index >= 0 
                ? new IndexTuple([index]) 
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(int index1, int index2)
        {
            return index1 >= 0 && index2 >= 0
                ? new IndexTuple([index1, index2])
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(int index1, int index2, int index3)
        {
            return index1 >= 0 && index2 >= 0 && index3 >= 0
                ? new IndexTuple([index1, index2, index3])
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(params int[] indexArray)
        {
            return indexArray.Length == 0
                ? EmptyTuple
                : new IndexTuple(indexArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(Span<int> indexArray)
        {
            return indexArray.Length == 0
                ? EmptyTuple
                : new IndexTuple(indexArray.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(ReadOnlySpan<int> indexArray)
        {
            return indexArray.Length == 0
                ? EmptyTuple
                : new IndexTuple(indexArray.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexTuple Create(IEnumerable<int> indexArray)
        {
            return Create(indexArray.ToArray());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(IndexTuple operand1, IndexTuple operand2)
        {
            return operand1.Equals(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(IndexTuple operand1, IndexTuple operand2)
        {
            return !operand1.Equals(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(IndexTuple operand1, IndexTuple operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(IndexTuple operand1, IndexTuple operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(IndexTuple operand1, IndexTuple operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(IndexTuple operand1, IndexTuple operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }


        private readonly int[] _indexArray;

        public bool IsEmptyTuple
            => _indexArray.Length == 0;

        public bool IsUnitTuple
            => Count == 1;

        public bool IsPairTuple
            => Count == 2;

        public bool IsTripletTuple
            => Count == 3;

        public int FirstIndex
            => _indexArray[0];

        public int LastIndex
            => _indexArray[^1];

        public Pair<int> IndexRange
            => new Pair<int>(_indexArray[0], _indexArray[^1]);

        public int Count
            => _indexArray.Length;

        public int this[int index]
            => _indexArray[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IndexTuple(int[] indexArray)
        {
            _indexArray = indexArray;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _indexArray.Length == 0 || _indexArray.All(index => index >= 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return obj is IndexTuple other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IndexTuple other)
        {
            if (IsEmptyTuple)
                return other.IsEmptyTuple;

            if (_indexArray.Length != other._indexArray.Length)
                return false;

            for (var i = 0; i < _indexArray.Length; i++)
            {
                if (_indexArray[i] != other._indexArray[i])
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            if (IsEmptyTuple) return 0;

            var hashCode = 0;
            foreach (var index in _indexArray)
                hashCode ^= index;

            return hashCode;
        }

        public int CompareTo(IndexTuple other)
        {
            var n1 = _indexArray.Length;
            var n2 = other._indexArray.Length;

            while (n1 > 0 && n2 > 0)
            {
                n1--;
                n2--;

                var index1 = _indexArray[n1];
                var index2 = other._indexArray[n2];

                if (index1 > index2) return 1;
                if (index1 < index2) return -1;
            }

            if (n1 == 0) return n2 == 0 ? 0 : -1;

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int[] GetInternalIndexArray()
        {
            return _indexArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<int> AsSpan()
        {
            return new ReadOnlySpan<int>(_indexArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexTuple GetSubset(int index, int count)
        {
            return count == 0
                ? EmptyTuple
                : Create(AsSpan().Slice(index, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<int> GetIndices()
        {
            return _indexArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<int> GetReversedIndices()
        {
            for (var i = _indexArray.Length - 1; i >= 0; i--)
                yield return _indexArray[i];
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<int> GetEnumerator()
        {
            return GetIndices().GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return GetIndices()
                .Select(i => i.ToString())
                .ConcatenateText(
                    ", ",
                    "IndexTuple<",
                    ">"
                );
        }
    }
}
