using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets
{
    public readonly struct IndexSet :
        IReadOnlyList<int>,
        IEquatable<IndexSet>,
        IComparable<IndexSet>
    {
        /// <summary>
        /// Search a sorted array of integers for a given integer using binary search.
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="value"></param>
        /// <returns>The index of the </returns>
        private static int GetItemIndex(ulong intArray, int value)
        {
            return (intArray & (1UL << value)) != 0
                ? BitOperations.PopCount((value + 1).CreateBitMaskUInt64() & intArray) - 1
                : throw new InvalidOperationException(); // Not found
        }

        /// <summary>
        /// Search a sorted array of integers for a given integer using binary search.
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="value"></param>
        /// <returns>The index of the </returns>
        private static int GetItemIndex(int[] intArray, int value)
        {
            var low = 0;
            var high = intArray.Length - 1;

            while (low <= high)
            {
                var mid = (int)((uint)low + (uint)(high - low) / 2);
                var midValue = intArray[mid];

                if (midValue < value)
                    low = mid + 1;
                else if (midValue > value)
                    high = mid - 1;
                else
                    return mid; // Found
            }

            throw new InvalidOperationException(); // Not found
        }

        /// <summary>
        /// Search a sorted array of integers for a given integer using binary search.
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="value"></param>
        /// <returns>The index of the </returns>
        private static int TryGetItemIndex(ulong intArray, int value)
        {
            return (intArray & (1UL << value)) != 0
                ? BitOperations.PopCount((value + 1).CreateBitMaskUInt64() & intArray) - 1
                : -1; // Not found
        }

        /// <summary>
        /// Search a sorted array of integers for a given integer using binary search.
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="value"></param>
        /// <returns>The index of the </returns>
        private static int TryGetItemIndex(int[] intArray, int value)
        {
            var low = 0;
            var high = intArray.Length - 1;

            while (low <= high)
            {
                var mid = (int)((uint)low + (uint)(high - low) / 2);
                var midValue = intArray[mid];

                if (midValue < value)
                    low = mid + 1;
                else if (midValue > value)
                    high = mid - 1;
                else
                    return mid; // Found
            }

            return ~low; // Not found, return bitwise complement of insertion point
        }

        /// <summary>
        /// Generates all combinations of size k from input index array
        /// </summary>
        /// <param name="indexArray"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static IEnumerable<int[]> GenerateCombinations(int[] indexArray, int k)
        {
            var n = indexArray.Length;

            Debug.Assert(k >= 0 && k <= n);

            var indices = new int[k];
            for (var i = 0; i < k; i++)
                indices[i] = i;

            while (true)
            {
                // Yield current combination
                var subset = new int[k];
                for (var i = 0; i < k; i++)
                    subset[i] = indexArray[indices[i]];
                yield return subset;

                // Move to next combination
                var iPos = k - 1;
                while (iPos >= 0 && indices[iPos] == n - k + iPos)
                    iPos--;

                if (iPos < 0) break;

                indices[iPos]++;
                for (var j = iPos + 1; j < k; j++)
                    indices[j] = indices[j - 1] + 1;
            }
        }


        private static int _emptySetCount;
        public static IndexSet EmptySet { get; }
            = new IndexSet(0UL);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateUnit(int index)
        {
            Debug.Assert(index >= 0);

            return index switch
            {
                < 0 => throw new InvalidOperationException(),
                < 64 => new IndexSet(1UL << index),
                _ => new IndexSet([index])
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreatePair(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index2 > index1);

            if (index1 < 0 || index2 < 0)
                throw new InvalidOperationException();

            if (index1 >= 64 || index2 >= 64)
                return new IndexSet([index1, index2]);

            return new IndexSet(
                (1UL << index1) | (1UL << index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateTriplet(int index1, int index2, int index3)
        {
            Debug.Assert(index1 >= 0 && index2 > index1 && index3 > index2);

            if (index1 < 0 || index2 < 0 || index3 < 0)
                throw new InvalidOperationException();

            if (index1 >= 64 || index2 >= 64 || index3 >= 64)
                return new IndexSet([index1, index2, index3]);

            return new IndexSet(
                (1UL << index1) | (1UL << index2) | (1UL << index3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Create(params int[] indexArray)
        {
            if (indexArray.Length == 0)
                return EmptySet;

            if (indexArray[0] < 0)
                throw new InvalidOperationException();

            if (indexArray[^1] >= 64)
                return new IndexSet(indexArray);

            var bitPattern = 0UL;

            foreach (var index in indexArray)
                bitPattern |= 1UL << index;

            return new IndexSet(bitPattern);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Create(Span<int> indexArray)
        {
            if (indexArray.Length == 0)
                return EmptySet;

            if (indexArray[0] < 0)
                throw new InvalidOperationException();

            if (indexArray[^1] >= 64)
                return new IndexSet(indexArray.ToArray());

            var bitPattern = 0UL;

            foreach (var index in indexArray)
                bitPattern |= 1UL << index;

            return new IndexSet(bitPattern);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Create(ReadOnlySpan<int> indexArray)
        {
            if (indexArray.Length == 0)
                return EmptySet;

            if (indexArray[0] < 0)
                throw new InvalidOperationException();

            if (indexArray[^1] >= 64)
                return new IndexSet(indexArray.ToArray());

            var bitPattern = 0UL;

            foreach (var index in indexArray)
                bitPattern |= 1UL << index;

            return new IndexSet(bitPattern);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Create(IReadOnlyList<int> indexArray, bool assumeOrderedDistinct)
        {
            return Create(
                assumeOrderedDistinct
                    ? indexArray.ToArray()
                    : indexArray.Distinct().OrderBy(i => i).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Create(IEnumerable<int> indexArray, bool assumeOrderedDistinct)
        {
            return Create(
                assumeOrderedDistinct
                    ? indexArray.ToArray()
                    : indexArray.Distinct().OrderBy(i => i).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateDense(int count)
        {
            if (count == 0) return EmptySet;

            var lastIndex = count - 1;

            if (lastIndex >= 64)
                return new IndexSet(
                    count.GetRange().ToArray()
                );

            return new IndexSet(
                count.CreateBitMaskUInt64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateDense(int firstIndex, int count)
        {
            if (firstIndex < 0)
                throw new InvalidOperationException();

            if (count == 0) return EmptySet;

            var lastIndex = firstIndex + count - 1;

            if (lastIndex >= 64)
                return new IndexSet(
                    count.GetRange(firstIndex).ToArray()
                );

            return new IndexSet(
                count.CreateBitMaskUInt64() << firstIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateFromUInt32Pattern(uint bitPattern)
        {
            return bitPattern == 0
                ? EmptySet
                : new IndexSet(bitPattern);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateFromUInt64Pattern(ulong bitPattern)
        {
            return bitPattern == 0
                ? EmptySet
                : new IndexSet(bitPattern);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet CreateFromUInt128Pattern(UInt128 bitPattern)
        {
            if (bitPattern <= ulong.MaxValue)
                return bitPattern == 0
                    ? EmptySet
                    : new IndexSet((ulong)bitPattern);

            var indexArray = 
                bitPattern.GetSetBitPositions().ToArray();

            return new IndexSet(indexArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet EncodeUInt64AsCombinadic(int intValue, int digitsCount)
        {
            return Create(
                ((ulong)intValue).IndexToCombinadic(digitsCount),
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet EncodeUInt64AsCombinadic(ulong intValue, int digitsCount)
        {
            return Create(
                intValue.IndexToCombinadic(digitsCount),
                false
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(IndexSet indexSet)
        {
            return indexSet.ToInt32();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint(IndexSet indexSet)
        {
            return indexSet.ToUInt32();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(IndexSet indexSet)
        {
            return indexSet.ToInt64();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ulong(IndexSet indexSet)
        {
            return indexSet.ToUInt64();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator IndexSet(int indexSet)
        {
            return indexSet == 0 ? EmptySet : new IndexSet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator IndexSet(ulong indexSet)
        {
            return indexSet == 0UL ? EmptySet : new IndexSet();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator <<(IndexSet operand1, int shift)
        {
            return operand1.ShiftIndices(shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator >>(IndexSet operand1, int shift)
        {
            return operand1.ShiftIndices(-shift);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator ^(ulong operand1, IndexSet operand2)
        {
            return CreateFromUInt64Pattern(operand1).SetMerge(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator ^(IndexSet operand1, ulong operand2)
        {
            return operand1.SetMerge(CreateFromUInt64Pattern(operand2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator ^(IndexSet operand1, IndexSet operand2)
        {
            return operand1.SetMerge(operand2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator &(ulong operand1, IndexSet operand2)
        {
            return CreateFromUInt64Pattern(operand1).SetIntersect(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator &(IndexSet operand1, ulong operand2)
        {
            return operand1.SetIntersect(CreateFromUInt64Pattern(operand2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator &(IndexSet operand1, IndexSet operand2)
        {
            return operand1.SetIntersect(operand2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator |(ulong operand1, IndexSet operand2)
        {
            return CreateFromUInt64Pattern(operand1).SetUnion(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator |(IndexSet operand1, ulong operand2)
        {
            return operand1.SetUnion(CreateFromUInt64Pattern(operand2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator |(IndexSet operand1, IndexSet operand2)
        {
            return operand1.SetUnion(operand2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator -(ulong operand1, IndexSet operand2)
        {
            return CreateFromUInt64Pattern(operand1).SetDifference(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator -(IndexSet operand1, ulong operand2)
        {
            return operand1.SetDifference(CreateFromUInt64Pattern(operand2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet operator -(IndexSet operand1, IndexSet operand2)
        {
            return operand1.SetDifference(operand2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(IndexSet operand1, IndexSet operand2)
        {
            return operand1.Equals(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(IndexSet operand1, IndexSet operand2)
        {
            return !operand1.Equals(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(IndexSet operand1, IndexSet operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(IndexSet operand1, IndexSet operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(IndexSet operand1, IndexSet operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(IndexSet operand1, IndexSet operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }


        private readonly ulong _bitPattern;
        private readonly int[] _indexArray;

        public bool IsEmptySet
            => _bitPattern == 0UL && _indexArray.Length == 0;

        public bool IsUnitSet
            => Count == 1;

        public bool IsPairSet
            => Count == 2;

        public bool IsTripletSet
            => Count == 3;

        public bool IsSparseSet
            => LastIndex - FirstIndex + 1 > Count;

        public bool IsDenseSet
            => LastIndex - FirstIndex + 1 == Count;

        public bool IsUInt64Set { get; }

        public int FirstIndex
            => _bitPattern == 0UL
                ? _indexArray[0]
                : _bitPattern.FirstOneBitPosition();

        public Pair<int> FirstIndexPair
            => _bitPattern == 0UL
                ? new Pair<int>(_indexArray[0], _indexArray[1])
                : new Pair<int>(_bitPattern.GetNthSetBitPosition(0), _bitPattern.GetNthSetBitPosition(1));

        public Triplet<int> FirstIndexTriplet
            => _bitPattern == 0UL
                ? new Triplet<int>(_indexArray[0], _indexArray[1], _indexArray[2])
                : new Triplet<int>(_bitPattern.GetNthSetBitPosition(0), _bitPattern.GetNthSetBitPosition(1), _bitPattern.GetNthSetBitPosition(2));

        public int LastIndex
            => _bitPattern == 0UL
                ? _indexArray[^1]
                : _bitPattern.LastOneBitPosition();

        public Pair<int> IndexRange
            => _bitPattern == 0UL
                ? new Pair<int>(_indexArray[0], _indexArray[^1])
                : new Pair<int>(_bitPattern.FirstOneBitPosition(), _bitPattern.LastOneBitPosition());

        public int Count
            => _bitPattern == 0UL
                ? _indexArray.Length
                : BitOperations.PopCount(_bitPattern);

        public int this[int index]
            => _bitPattern == 0UL
                ? _indexArray[index]
                : _bitPattern.GetNthSetBitPosition(index);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IndexSet(ulong bitPattern)
        {
            _bitPattern = bitPattern;
            _indexArray = [];
            IsUInt64Set = true;

            if (bitPattern == 0UL) _emptySetCount++;

            Debug.Assert(_emptySetCount <= 1);
            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IndexSet(int[] indexArray)
        {
            _bitPattern = 0;
            _indexArray = indexArray;
            IsUInt64Set = indexArray.Length == 0;

            if (indexArray.Length == 0) _emptySetCount++;

            Debug.Assert(_emptySetCount <= 1);
            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            if (_bitPattern != 0UL) return _indexArray.Length == 0;
            if (_indexArray.Length == 0) return true;
            if (_indexArray[^1] < 64) return false;

            var firstIndex = _indexArray[0];
            if (firstIndex < 0) return false;

            for (var i = 1; i < _indexArray.Length; i++)
            {
                var nextIndex = _indexArray[i];
                if (nextIndex <= firstIndex) return false;

                firstIndex = nextIndex;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return obj is IndexSet other && Equals(other);
        }

        public bool Equals(IndexSet other)
        {
            if (IsEmptySet)
                return other.IsEmptySet;

            if (IsUInt64Set)
                return other.IsUInt64Set && _bitPattern == other._bitPattern;

            if (other.IsUInt64Set)
                return IsUInt64Set && _bitPattern == other._bitPattern;

            if (_indexArray.Length != other._indexArray.Length)
                return false;

            for (var i = 0; i < _indexArray.Length; i++)
            {
                if (_indexArray[i] != other._indexArray[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            if (IsEmptySet) return 0;

            var hashCode = 0;

            if (IsUInt64Set)
            {
                var bitPattern = _bitPattern;
                while (bitPattern != 0)
                {
                    var index = BitOperations.TrailingZeroCount(bitPattern);
                    bitPattern &= bitPattern - 1;

                    hashCode ^= index;
                }
            }
            else
            {
                foreach (var index in _indexArray)
                    hashCode ^= index;
            }

            return hashCode;
        }

        public int CompareTo(IndexSet other)
        {
            if (IsUInt64Set)
            {
                return other.IsUInt64Set
                    ? _bitPattern.CompareTo(other._bitPattern)
                    : -1;
            }

            if (other.IsUInt64Set) return 1;

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
        public int GetIndexPosition(int index)
        {
            return IsUInt64Set
                ? GetItemIndex(_bitPattern, index)
                : GetItemIndex(_indexArray, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int TryGetIndexPosition(int index)
        {
            return IsUInt64Set
                ? TryGetItemIndex(_bitPattern, index)
                : TryGetItemIndex(_indexArray, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int[] GetInternalIndexArray()
        {
            return _indexArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<int> AsSpan()
        {
            return IsUInt64Set
                ? _bitPattern.GetSetBitPositionsAsSpan()
                : new ReadOnlySpan<int>(_indexArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet GetSubset(int index, int count)
        {
            return count == 0
                ? EmptySet
                : Create(AsSpan().Slice(index, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<int> GetIndices()
        {
            return IsUInt64Set
                ? _bitPattern.GetSetBitPositions()
                : _indexArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetIndices<T>(Func<int, T> indexMapFunc)
        {
            return IsUInt64Set
                ? _bitPattern.GetSetBitPositions().Select(indexMapFunc)
                : _indexArray.Select(indexMapFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<int> GetReversedIndices()
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSetBitPositionsReversed())
                    yield return i;
            }
            else
            {
                for (var i = _indexArray.Length - 1; i >= 0; i--)
                    yield return _indexArray[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetReversedIndices<T>(Func<int, T> indexMapFunc)
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSetBitPositionsReversed())
                    yield return indexMapFunc(i);
            }
            else
            {
                for (var i = _indexArray.Length - 1; i >= 0; i--)
                    yield return indexMapFunc(_indexArray[i]);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexSet> GetUnitSubsets()
        {
            return IsUInt64Set
                ? _bitPattern.GetSetBitPositions().Select(i => new IndexSet(1UL << i))
                : _indexArray.Select(i => i < 64 ? new IndexSet(1UL << i) : new IndexSet([i]));
        }

        public IEnumerable<IndexSet> GetSubsets()
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSubBitPatterns())
                    yield return CreateFromUInt64Pattern(i);
            }
            else
            {
                var n = _indexArray.Length;

                yield return EmptySet;

                for (var k = 1; k < n; k++)
                {
                    var subSets =
                        GenerateCombinations(_indexArray, k);

                    foreach (var subset in subSets)
                        yield return Create(subset);
                }

                yield return this;
            }
        }

        public IEnumerable<IndexSet> GetSubsetsOfSize(int size)
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSubBitPatternsOfSize(size))
                    yield return new IndexSet(i);
            }
            else
            {
                if (size < 0 || size > _indexArray.Length) yield break;

                if (size == 0)
                {
                    yield return EmptySet;
                    yield break;
                }

                if (size == _indexArray.Length)
                {
                    yield return this;
                    yield break;
                }

                foreach (var subset in GenerateCombinations(_indexArray, size))
                    yield return Create(subset);
            }
        }

        public IEnumerable<IndexSet> GetSubsetsOfSizeInRange(int maxSize)
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSubBitPatternsOfSizeInRange(0, maxSize))
                    yield return new IndexSet(i);
            }
            else
            {
                var k2 = int.Min(maxSize, _indexArray.Length);

                yield return EmptySet;

                for (var k = 1; k < k2; k++)
                {
                    foreach (var subset in GenerateCombinations(_indexArray, k))
                        yield return Create(subset);
                }

                if (k2 == _indexArray.Length)
                    yield return this;
                else
                {
                    foreach (var subset in GenerateCombinations(_indexArray, k2))
                        yield return Create(subset);
                }
            }
        }

        public IEnumerable<IndexSet> GetSubsetsOfSizeInRange(int minSize, int maxSize)
        {
            if (IsUInt64Set)
            {
                foreach (var i in _bitPattern.GetSubBitPatternsOfSizeInRange(minSize, maxSize))
                    yield return new IndexSet(i);
            }
            else
            {
                var k1 = int.Max(minSize, 0);
                var k2 = int.Min(maxSize, _indexArray.Length);

                if (k1 == 0)
                    yield return EmptySet;
                else
                {
                    foreach (var subset in GenerateCombinations(_indexArray, k1))
                        yield return Create(subset);
                }

                for (var k = k1 + 1; k < k2; k++)
                {
                    foreach (var subset in GenerateCombinations(_indexArray, k))
                        yield return Create(subset);
                }

                if (k2 == _indexArray.Length)
                    yield return this;
                else
                {
                    foreach (var subset in GenerateCombinations(_indexArray, k2))
                        yield return Create(subset);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToInt32()
        {
            if (IsEmptySet) return 0;

            return IsUInt64Set && LastIndex < 31
                ? (int)_bitPattern
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ToUInt32()
        {
            if (IsEmptySet) return 0;

            return IsUInt64Set && LastIndex < 32
                ? (uint)_bitPattern
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToInt64()
        {
            if (IsEmptySet) return 0;

            return IsUInt64Set && LastIndex < 63
                ? (long)_bitPattern
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong ToUInt64()
        {
            if (IsEmptySet) return 0;

            return IsUInt64Set
                ? _bitPattern
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt128 ToUInt128()
        {
            if (IsEmptySet) 
                return 0;

            if (IsUInt64Set) 
                return _bitPattern;

            if (LastIndex < 128)
                return _indexArray.Aggregate(
                    UInt128.Zero,
                    (currentBitPattern, bitPosition) => 
                        currentBitPattern | (UInt128.One << bitPosition)
                );

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (bool found, ulong bitPattern) TryGetUInt64()
        {
            return IsUInt64Set
                ? (true, _bitPattern)
                : (false, 0UL);
        }


        /// <summary>
        /// https://en.wikipedia.org/wiki/Combinatorial_number_system
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int DecodeCombinadicToInt32()
        {
            return (int)DecodeCombinadicToUInt64();
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Combinatorial_number_system
        /// </summary>
        /// <returns></returns>
        public ulong DecodeCombinadicToUInt64()
        {
            if (IsEmptySet) return 0;
            if (IsUnitSet) return (ulong)FirstIndex;

            var intValue = 0UL;

            if (IsUInt64Set)
            {
                var bitPattern = _bitPattern;
                var k = 0;
                var n = 0;

                while (bitPattern > 0)
                {
                    if ((bitPattern & 1UL) != 0)
                    {
                        k++; // binomial lower index: 1-based index
                        intValue += n.GetBinomialCoefficient(k);
                    }

                    n++; // binomial upper index
                    bitPattern >>= 1;
                }
            }
            else
            {
                for (var i = 0; i < _indexArray.Length; i++)
                    intValue += _indexArray[i].GetBinomialCoefficient(i + 1);
            }

            return intValue;
        }


        public bool SetContains(int index)
        {
            if (IsUInt64Set)
                return index is >= 0 and < 64 && (_bitPattern & (1UL << index)) != 0;

            var low = 0;
            var high = _indexArray.Length - 1;

            while (low <= high)
            {
                var mid = (int)((uint)low + (uint)(high - low) / 2);
                var midValue = _indexArray[mid];

                if (midValue < index)
                    low = mid + 1;
                else if (midValue > index)
                    high = mid - 1;
                else
                    return true; // Found
            }

            return false; // Not found, return bitwise complement of insertion point
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetContainsSingleIndex(int index)
        {
            return IsUInt64Set
                ? index is >= 0 and < 64 && _bitPattern == 1UL << index
                : _indexArray.Length == 1 && _indexArray[0] == index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetIsSubsetOf(IndexSet indexSet2)
        {
            return indexSet2.SetIsSupersetOf(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetIsSupersetOf(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.IsSuperset(this, indexSet2)
                    : IndexSetPatternArrayUtils.IsSuperset(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.IsSuperset(this, indexSet2)
                : IndexSetArrayArrayUtils.IsSuperset(this, indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetContains(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Contains(this, indexSet2)
                    : IndexSetPatternArrayUtils.Contains(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Contains(this, indexSet2)
                : IndexSetArrayArrayUtils.Contains(this, indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetOverlaps(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Overlaps(this, indexSet2)
                    : IndexSetPatternArrayUtils.Overlaps(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Overlaps(this, indexSet2)
                : IndexSetArrayArrayUtils.Overlaps(this, indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetOverlaps(HashSet<int> indexSet2)
        {
            if (IsEmptySet || indexSet2.Count == 0)
                return false;

            return this.Any(indexSet2.Contains);
            //return Count <= indexSet2.Count 
            //    ? this.Any(indexSet2.Contains) // Array is smaller or equal in size - iterate through array
            //    : indexSet2.Any(item => Array.BinarySearch(this.ToArray(), item) >= 0); // Hash set is smaller - iterate through hash set
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetDisjoint(IndexSet indexSet2)
        {
            return !SetOverlaps(indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet MapIndicesByValue(Func<int, int> indexMapping)
        {
            if (IsEmptySet) return this;

            return Create(
                GetIndices().Select(indexMapping).Distinct().OrderBy(i => i).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet MapIndicesByOrderValue(Func<int, int, int> indexMapping)
        {
            if (IsEmptySet) return this;

            return Create(
                GetIndices().Select((index, order) => indexMapping(order, index)).Distinct().OrderBy(i => i).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet ShiftIndices(int offset)
        {
            if (IsEmptySet || offset == 0) return this;

            if (IsUInt64Set)
            {
                if (offset < 0)
                    return -offset <= BitOperations.TrailingZeroCount(_bitPattern)
                        ? new IndexSet(_bitPattern >> -offset)
                        : throw new InvalidOperationException();

                if (offset <= BitOperations.LeadingZeroCount(_bitPattern))
                    return new IndexSet(_bitPattern << offset);
            }

            return new IndexSet(
                GetIndices().Select(i => i + offset).ToArray()
            );
        }


        public IndexSet Insert(int index)
        {
            if (IsUInt64Set && index is >= 0 and < 64)
            {
                return (_bitPattern & 1UL << index) == 0
                    ? new IndexSet(_bitPattern | 1UL << index)
                    : throw new InvalidOperationException();
            }

            var indexArray = IsUInt64Set
                ? _bitPattern.GetSetBitPositionsAsArray()
                : _indexArray;

            // Step 1: Binary search to find index
            var indexPosition = TryGetItemIndex(indexArray, index);

            if (indexPosition >= 0)
            {
                // Value already exists
                throw new InvalidOperationException();
            }

            // Value not found; compute insertion point
            var insertionIndex = ~indexPosition;

            // Step 2: Allocate new buffer
            var buffer = new int[indexArray.Length + 1];

            // Step 3: Copy prefix
            if (insertionIndex > 0)
                indexArray[..insertionIndex].CopyTo(buffer, 0);

            // Step 4: Insert new index
            buffer[insertionIndex] = index;

            // Step 5: Copy suffix
            if (insertionIndex < indexArray.Length)
                indexArray[insertionIndex..].CopyTo(buffer, insertionIndex + 1);

            return new IndexSet(buffer);
        }

        public IndexSet TryInsert(int index)
        {
            if (IsUInt64Set && index is >= 0 and < 64)
            {
                return (_bitPattern & 1UL << index) == 0
                    ? new IndexSet(_bitPattern | 1UL << index)
                    : this;
            }

            var indexArray = IsUInt64Set
                ? _bitPattern.GetSetBitPositionsAsArray()
                : _indexArray;

            // Step 1: Binary search to find index
            var indexPosition = TryGetItemIndex(indexArray, index);

            if (indexPosition >= 0)
            {
                // Value already exists
                return this;
            }

            // Value not found; compute insertion point
            var insertionIndex = ~indexPosition;

            // Step 2: Allocate new buffer
            var buffer = new int[indexArray.Length + 1];

            // Step 3: Copy prefix
            if (insertionIndex > 0)
                indexArray[..insertionIndex].CopyTo(buffer, 0);

            // Step 4: Insert new index
            buffer[insertionIndex] = index;

            // Step 5: Copy suffix
            if (insertionIndex < indexArray.Length)
                indexArray[insertionIndex..].CopyTo(buffer, insertionIndex + 1);

            return new IndexSet(buffer);
        }

        public IndexSet Remove(int index)
        {
            if (IsUInt64Set)
                return index is >= 0 and < 64 && (_bitPattern & (1UL << index)) != 0
                    ? new IndexSet(_bitPattern & ~(1UL << index))
                    : throw new InvalidOperationException();

            // Step 1: Binary search to find indexPosition
            var indexPosition = TryGetItemIndex(_indexArray, index);

            if (indexPosition < 0)
            {
                // Value doesn't exist
                throw new InvalidOperationException();
            }

            // Step 2: Allocate new buffer
            var buffer = new int[_indexArray.Length - 1];

            // Step 3: Copy prefix
            if (indexPosition > 0)
                _indexArray[..indexPosition].CopyTo(buffer, 0);

            // Step 4: Copy suffix
            if (indexPosition < _indexArray.Length - 1)
                _indexArray[(indexPosition + 1)..].CopyTo(buffer, indexPosition);

            return Create(buffer);
        }

        public IndexSet TryRemove(int index)
        {
            if (IsUInt64Set)
                return index is >= 0 and < 64 && (_bitPattern & (1UL << index)) != 0
                    ? new IndexSet(_bitPattern & ~(1UL << index))
                    : this;

            // Step 1: Binary search to find indexPosition
            var indexPosition = TryGetItemIndex(_indexArray, index);

            if (indexPosition < 0)
            {
                // Value doesn't exist
                return this;
            }

            // Step 2: Allocate new buffer
            var buffer = new int[_indexArray.Length - 1];

            // Step 3: Copy prefix
            if (indexPosition > 0)
                _indexArray[..indexPosition].CopyTo(buffer, 0);

            // Step 4: Copy suffix
            if (indexPosition < _indexArray.Length - 1)
                _indexArray[(indexPosition + 1)..].CopyTo(buffer, indexPosition);

            return Create(buffer);
        }


        public IndexSet SetComplement()
        {
            if (IsEmptySet)
                return this;

            var maxIndex = LastIndex;

            if (IsUInt64Set)
                return CreateFromUInt64Pattern(
                    ~_bitPattern & ((1UL << (maxIndex + 1)) - 1)
                );

            var result = new List<int>(
                maxIndex - _indexArray.Length + 1
            );

            var i = 0;
            for (var candidate = 0; candidate < maxIndex; candidate++)
            {
                if (i < _indexArray.Length && _indexArray[i] == candidate)
                {
                    i++; // skip numbers present in index set
                    continue;
                }

                result.Add(candidate);
            }

            return Create(result, true);
        }
        
        public IndexSet SetComplement(int maxIndex)
        {
            if (maxIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(maxIndex));

            if (IsEmptySet)
                return CreateDense(maxIndex + 1);

            if (maxIndex < LastIndex)
                throw new InvalidOperationException();

            if (maxIndex == LastIndex)
                return SetComplement();

            if (IsUInt64Set)
                return maxIndex <= 63
                    ? CreateFromUInt64Pattern(
                        ~_bitPattern & ((1UL << (maxIndex + 1)) - 1)
                    )
                    : new IndexSet(
                        (~_bitPattern)
                        .GetSetBitPositions()
                        .Concat((maxIndex - 63).GetRange(64))
                        .ToArray()
                    );

            var result = new List<int>(
                maxIndex - _indexArray.Length + 1
            );

            var i = 0;
            for (var candidate = 0; candidate <= maxIndex; candidate++)
            {
                if (i < _indexArray.Length && _indexArray[i] == candidate)
                {
                    i++; // present in S, skip
                    continue;
                }

                result.Add(candidate);
            }

            return Create(result, true);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetIntersect(int index2)
        {
            Debug.Assert(index2 >= 0);

            return SetContains(index2) ? CreateUnit(index2) : EmptySet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetIntersect(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Intersect(this, indexSet2)
                    : IndexSetPatternArrayUtils.Intersect(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Intersect(this, indexSet2)
                : IndexSetArrayArrayUtils.Intersect(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetUnion(int index2)
        {
            Debug.Assert(index2 >= 0);

            if (IsUInt64Set)
            {
                return index2 < 64
                    ? IndexSetPatternPatternUtils.Join(this, index2)
                    : IndexSetPatternArrayUtils.Join(this, index2);
            }

            return IndexSetArrayArrayUtils.Join(this, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetUnion(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Join(this, indexSet2)
                    : IndexSetPatternArrayUtils.Join(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Join(this, indexSet2)
                : IndexSetArrayArrayUtils.Join(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetMerge(int index2)
        {
            Debug.Assert(index2 >= 0);

            if (IsUInt64Set)
            {
                return index2 < 64
                    ? IndexSetPatternPatternUtils.Merge(this, index2)
                    : IndexSetPatternArrayUtils.Merge(this, index2);
            }

            return IndexSetArrayArrayUtils.Merge(this, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetMerge(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Merge(this, indexSet2)
                    : IndexSetPatternArrayUtils.Merge(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Merge(this, indexSet2)
                : IndexSetArrayArrayUtils.Merge(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetDifference(int index2)
        {
            return TryRemove(index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet SetDifference(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.Difference(this, indexSet2)
                    : IndexSetPatternArrayUtils.Difference(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.Difference(this, indexSet2)
                : IndexSetArrayArrayUtils.Difference(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SetCountSwaps(int index2)
        {
            Debug.Assert(index2 >= 0);

            if (!IsUInt64Set)
                return IndexSetArrayArrayUtils.CountSwaps(this, index2);

            return index2 < 64
                ? IndexSetPatternPatternUtils.CountSwaps(this, index2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SetCountSwaps(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.CountSwaps(this, indexSet2)
                    : IndexSetPatternArrayUtils.CountSwaps(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.CountSwaps(this, indexSet2)
                : IndexSetArrayArrayUtils.CountSwaps(this, indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SetCountSwapsWithSelf()
        {
            return IsUInt64Set
                ? IndexSetPatternPatternUtils.CountSwapsWithSelf(this)
                : IndexSetArrayArrayUtils.CountSwapsWithSelf(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int swapCount, IndexSet mergedIndexSet) SetMergeCountSwaps(int index2)
        {
            Debug.Assert(index2 >= 0);

            if (!IsUInt64Set)
                return IndexSetArrayArrayUtils.MergeCountSwaps(this, index2);

            return index2 < 64
                ? IndexSetPatternPatternUtils.MergeCountSwaps(this, index2)
                : IndexSetPatternArrayUtils.MergeCountSwaps(this, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int swapCount, IndexSet mergedIndexSet) SetMergeCountSwaps(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.MergeCountSwaps(this, indexSet2)
                    : IndexSetPatternArrayUtils.MergeCountSwaps(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.MergeCountSwaps(this, indexSet2)
                : IndexSetArrayArrayUtils.MergeCountSwaps(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) SetMergeCountSwapsTrackCommon(int index2)
        {
            Debug.Assert(index2 >= 0);

            if (!IsUInt64Set)
                return IndexSetArrayArrayUtils.MergeCountSwapsTrackCommon(this, index2);

            return index2 < 64
                ? IndexSetPatternPatternUtils.MergeCountSwapsTrackCommon(this, index2)
                : IndexSetPatternArrayUtils.MergeCountSwapsTrackCommon(this, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) SetMergeCountSwapsTrackCommon(IndexSet indexSet2)
        {
            if (IsUInt64Set)
            {
                return indexSet2.IsUInt64Set
                    ? IndexSetPatternPatternUtils.MergeCountSwapsTrackCommon(this, indexSet2)
                    : IndexSetPatternArrayUtils.MergeCountSwapsTrackCommon(this, indexSet2);
            }

            return indexSet2.IsUInt64Set
                ? IndexSetArrayPatternUtils.MergeCountSwapsTrackCommon(this, indexSet2)
                : IndexSetArrayArrayUtils.MergeCountSwapsTrackCommon(this, indexSet2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (IndexSet unitIndexSet, IndexSet remainingIndexSet) SplitByFirstIndex()
        {
            if (IsEmptySet)
                throw new InvalidOperationException();

            if (IsUnitSet)
                return (this, EmptySet);

            var firstIndex = FirstIndex;
            var unitIndexSet = CreateUnit(firstIndex);
            var remainingIndexSet = Remove(firstIndex);

            return (unitIndexSet, remainingIndexSet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (IndexSet unitIndexSet, IndexSet remainingIndexSet) SplitByLastIndex()
        {
            if (IsEmptySet)
                throw new InvalidOperationException();

            if (IsUnitSet)
                return (this, EmptySet);

            var lastIndex = LastIndex;
            var unitIndexSet = CreateUnit(lastIndex);
            var remainingIndexSet = Remove(lastIndex);

            return (unitIndexSet, remainingIndexSet);
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
                    IsUInt64Set ? "UInt64IndexSet<" : "ArrayIndexSet<",
                    ">"
                );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet FlipIndices()
        {
            var setSize = Count;

            return setSize <= 64
                ? new IndexSet(
                    _bitPattern.ReversePrefixBits(setSize)
                )
                : new IndexSet(
                    GetIndices(i => setSize - i).ToArray()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet FlipIndices(int setSize)
        {
            if (setSize < Count)
                throw new InvalidOperationException();

            return setSize <= 64
                ? new IndexSet(
                    _bitPattern.ReversePrefixBits(setSize)
                )
                : new IndexSet(
                    GetIndices(i => setSize - i).ToArray()
                );
        }


        /// <summary>
        /// Returns the next index set in lexicographic order.
        /// For example: IS(0,1,2) -> IS(3), IS(3) -> IS(0,3), etc.
        /// </summary>
        /// <returns>The next index set</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet GetNext()
        {
            // For array-based sets, we need to find the next lexicographic arrangement
            if (!IsUInt64Set)
                return GetNextForArray();

            // For UInt64 patterns, simply increment the bit pattern
            return _bitPattern == ulong.MaxValue
                ? CreateUnit(64)
                : CreateFromUInt64Pattern(_bitPattern + 1);

        }

        /// <summary>
        /// Returns the next index set with the same size in lexicographic order.
        /// For example: IS(0,1) -> IS(0,2), IS(0,2) -> IS(1,2), etc.
        /// </summary>
        /// <returns>The next index set with same size, or null if no more exist</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet GetNextSameSize()
        {
            if (IsEmptySet)
                throw new InvalidOperationException("Empty set has no next of same size");

            return IsUInt64Set
                ? GetNextSameSizeForPattern()
                : GetNextSameSizeForArray();
        }

        private IndexSet GetNextForArray()
        {
            // Handle empty array - next pattern is {0}
            if (_indexArray.Length == 0)
                return CreateUnit(0);

            // Implement binary addition with carry propagation
            // Start from bit position 0 and work up
            var bitSet = new HashSet<int>(_indexArray);
            var carryPosition = 0;

            // Perform binary addition by 1
            while (true)
            {
                // If this bit is 0, set it to 1 and we're done
                if (bitSet.Add(carryPosition))
                    break;

                // This bit is 1, flip it to 0 and carry to next position
                bitSet.Remove(carryPosition);

                // Check for overflow at maximum position
                if (carryPosition >= int.MaxValue)
                    throw new OverflowException("Next pattern would exceed maximum representable value");

                carryPosition++;
            }

            // Convert back to sorted array
            var result = bitSet.ToArray();
            Array.Sort(result);

            return new IndexSet(result);
        }
        
        private IndexSet GetNextSameSizeForPattern()
        {
            var size = BitOperations.PopCount(_bitPattern);
            var trailingZeros = BitOperations.TrailingZeroCount(_bitPattern);

            // Reached limit of bit pattern index sets; so the next index set
            // must be of array type
            if (size + trailingZeros == 64)
            {
                var indexArray = new int[size];

                for (var i = 0; i < size - 1; i++)
                    indexArray[i] = i;

                indexArray[size - 1] = 64;

                return new IndexSet(indexArray);
            }

            // Find the rightmost set bit
            var rightmostSetBit = 1UL << trailingZeros;

            // Ripple: add the rightmost set bit
            var ripple = _bitPattern + rightmostSetBit;

            // Calculate the ones that need to be moved
            var ones = ((_bitPattern ^ ripple) >> 2) / rightmostSetBit;

            return CreateFromUInt64Pattern(ripple | ones);
        }

        private IndexSet GetNextSameSizeForArray()
        {
            var n = _indexArray.Length;

            if (n == 0)
                throw new InvalidOperationException();
        
            if (n == 1)
                return CreateUnit(_indexArray[0] + 1);
        
            if (n == 2)
            {
                var i1 = _indexArray[0];
                var i2 = _indexArray[1];
                
                i1++;
                if (i1 == i2)
                {
                    i1 = 0;
                    i2++;
                }
                
                return CreatePair(i1, i2);
            }
        
            if (n == 3)
            {
                var i1 = _indexArray[0];
                var i2 = _indexArray[1];
                var i3 = _indexArray[2];

                i1++;
                if (i1 == i2)
                {
                    i1 = 0;

                    i2++;
                    if (i2 == i3)
                    {
                        i2 = 1;
                        i3++;
                    }
                }
                
                return CreateTriplet(i1, i2, i3);
            }

            var indexArray = new int[n];
            Array.Copy(_indexArray, indexArray, n);
        
            // Find the leftmost position i with a gap to its right: _indexArray[i] + 1 < _indexArray[i+1].
            // If found, increment p[i], reset all lower indices to minimal [0..i-1], keep the rest.
            for (var i = 0; i < n - 1; i++)
            {
                if (indexArray[i] + 1 >= indexArray[i + 1]) continue;

                indexArray[i] += 1;
                for (var j = 0; j < i; j++) 
                    indexArray[j] = j; // bottom-pack to minimize value
            
                return new IndexSet(indexArray);
            }
        
            // No gap: all adjacent are consecutive. Move the MSB up by one and pack the rest to bottom.
            for (var j = 0; j < n - 1; j++) 
                indexArray[j] = j;

            indexArray[n - 1] = _indexArray[n - 1] + 1;

            return new IndexSet(indexArray);
        }

        private IndexSet GetNextSameSizeForArray1()
        {
            var k = _indexArray.Length;
            var result = new int[k];

            // Create working copy
            Array.Copy(_indexArray, result, k);

            // Start with incrementing the first position
            result[0]++;

            // Propagate carries from left to right
            for (var i = 0; i < k - 1; i++)
            {
                // If current position >= next position OR exceeds max allowed
                var maxAllowed = int.MaxValue - (k - 1 - i);
                if (result[i] >= result[i + 1] || result[i] > maxAllowed)
                {
                    // Reset current position to the minimum possible value
                    result[i] = (i == 0) ? 0 : result[i - 1] + 1;

                    // Increment the next position
                    result[i + 1]++;

                    continue;
                }

                // Current position is valid, ensure next position is at least current + 1
                if (result[i + 1] <= result[i])
                    result[i + 1] = result[i] + 1;
            }

            // Handle the last position
            var lastMax = int.MaxValue - (k - 1 - (k - 1));

            if (result[k - 1] <= lastMax)
                return new IndexSet(result);

            // Overflow in last position - this means we've reached the end
            // Return first combination
            //return CreateDense(k);

            throw new InvalidOperationException("No next pattern with same size exists");
        }



        /// <summary>
        /// Returns the previous index set in lexicographic order.
        /// For example: IS(0,1,2) -> IS(3), IS(3) -> IS(0,3), etc.
        /// </summary>
        /// <returns>The next index set</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet GetPrev()
        {
            if (IsEmptySet)
                throw new InvalidOperationException("Empty set has no previous");

            return IsUInt64Set
                ? CreateFromUInt64Pattern(_bitPattern - 1)
                : GetPrevForArray();
        }

        /// <summary>
        /// Returns the previous index set with the same size in lexicographic order.
        /// For example: IS(0,1) -> IS(0,2), IS(0,2) -> IS(1,2), etc.
        /// </summary>
        /// <returns>The next index set with same size, or null if no more exist</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexSet GetPrevSameSize()
        {
            if (IsEmptySet)
                throw new InvalidOperationException("Empty set has no previous of same size");

            if (IsDenseSet && FirstIndex == 0)
                throw new InvalidOperationException("Set has no previous of same size");

            var n = LastIndex;
            return SetComplement().GetNextSameSize().SetComplement(n);

            //return IsUInt64Set
            //    ? GetPrevSameSizeForPattern()
            //    : GetPrevSameSizeForArray();
        }

        private IndexSet GetPrevForArray()
        {
            // Handle pattern {0} - previous would be empty array (represents 0-1=-1, invalid)
            if (_indexArray is [0])
                return EmptySet;

            // Implement binary subtraction (subtract 1)
            // Start from bit position 0 and work up
            var bitSet = new HashSet<int>(_indexArray);
            var borrowPosition = 0;

            // Perform binary subtraction by 1
            while (true)
            {
                if (!bitSet.Add(borrowPosition))
                {
                    // This bit is 1, flip it to 0, and we're done (no borrow needed)
                    bitSet.Remove(borrowPosition);
                    break;
                }

                // This bit is 0, set it to 1 and continue borrowing from next position
                borrowPosition++;

                // Check if we need to borrow beyond the highest set bit
                if (borrowPosition > _indexArray[^1])
                    throw new InvalidOperationException("Previous pattern would result in negative number");
            }

            // Convert back to sorted array
            var result = bitSet.ToArray();
            Array.Sort(result);

            return new IndexSet(result);
        }

        private IndexSet GetPrevSameSizeForPattern()
        {
            var n = Count;

            // Fast path for single element
            if (n == 1)
            {
                var pos = _bitPattern.FirstOneBitPosition();
                return pos > 0
                    ? CreateUnit(pos - 1)
                    : throw new InvalidOperationException();
            }

            // Find the previous number with the same pop count (ultra-optimized)
            var previousValue = 
                GetPreviousBitPattern_BitOperationsOptimized();

            // Handle special case where BitOperations couldn't create valid bit pattern
            // Fall back to array implementation for edge cases
            if (previousValue == 0 && _bitPattern != 0)
                return GetPrevSameSizeForArray();

            // Convert back to bit positions using hardware-accelerated operations
            var result = 0UL;
            var index = 0;

            var temp = previousValue;
            while (temp != 0 && index < n)
            {
                var bitPos = BitOperations.TrailingZeroCount(temp);
                result = result.SetBitToOneAt(bitPos);
                index++;
                temp &= temp - 1; // Clear lowest set bit
            }

            return CreateFromUInt64Pattern(result);
        }

        // Ultra-optimized bit manipulation core using BitOperations
        private ulong GetPreviousBitPattern_BitOperationsOptimized()
        {
            if (_bitPattern == 0) return 0;

            // Use hardware-accelerated operations for faster bit manipulation
            var rightmostMovable = -1;

            // Find the rightmost set bit that can be moved left (has a gap to its left)
            // This matches the array implementation logic exactly
            for (var i = 1; i < 64; i++)
            {
                // Bit i is set
                if ((_bitPattern & (1UL << i)) == 0) continue; 
                
                // Check if there's a gap (unset bit) to the left of position i
                // Left immediate bit is 0
                if ((_bitPattern & (1UL << (i - 1))) != 0) continue; 
                
                // Take the first (rightmost) one we find
                rightmostMovable = i;
                break; 
            }

            if (rightmostMovable == -1)
            {
                // No movable bit found - return max combination ending at highest_bit - 1
                // Use hardware pop count and leading zero count for efficiency
                var popCount = BitOperations.PopCount(_bitPattern);
                var leadingZeros = BitOperations.LeadingZeroCount(_bitPattern);
                var highestBit = 63 - leadingZeros;

                // Check if we can create a valid max combination
                if (highestBit - 1 - (popCount - 1) < 0)
                {
                    // Would require negative bit positions - fallback to array implementation approach
                    // This handles edge cases like [0, 1] -> [-1, 0]
                    return 0; // Return 0 to indicate special case that needs array conversion
                }

                // Create max combination ending at highestBit - 1
                ulong result = 0;

                for (var i = 0; i < popCount; i++)
                {
                    var bitPos = highestBit - 1 - i;
                    if (bitPos >= 0) result |= 1UL << bitPos;
                }

                return result;
            }

            // Count set bits to the right of rightmostMovable using hardware popcount
            var mask = (1UL << rightmostMovable) - 1;
            var setBitsToRight = BitOperations.PopCount(_bitPattern & mask);

            // Clear the rightmost movable bit
            var bitPattern = _bitPattern & ~(1UL << rightmostMovable);

            // Clear all bits to the right of rightmostMovable
            bitPattern &= ~mask;

            // Set a bit at rightmostMovable - 1
            bitPattern |= 1UL << (rightmostMovable - 1);

            // Set the remaining setBitsToRight bits at their highest positions
            // consecutively to the right of the moved bit
            for (var i = 0; i < setBitsToRight; i++) 
                bitPattern |= 1UL << (rightmostMovable - 2 - i);

            return bitPattern;
        }


        private IndexSet GetPrevSameSizeForArray()
        {
            var k = _indexArray.Length;

            // Create a boolean array to represent which positions are set
            var maxPos = _indexArray.Max();
            var positions = new bool[maxPos + 1];
            foreach (var pos in _indexArray)
            {
                positions[pos] = true;
            }

            // Find the rightmost set position that can be moved left (has gap to its left)
            var rightmostMovable = -1;

            for (var i = 1; i <= maxPos; i++)
            {
                // position i is set and position i-1 is unset
                if (!positions[i] || positions[i - 1]) continue;

                // Take the first (rightmost) one we find
                rightmostMovable = i;
                break;
            }

            if (rightmostMovable == -1)
            {
                // No movable position found - return max combination ending at maxPos - 1
                var result = new int[k];

                for (var i = 0; i < k; i++)
                    result[i] = maxPos - 1 - (k - 1 - i);

                return Create(result);
            }

            // Count set positions to the right of rightmostMovable
            var setBitsToRight = 0;
            for (var i = 0; i < rightmostMovable; i++)
                if (positions[i]) setBitsToRight++;

            // Clear all positions at and to the right of rightmostMovable
            for (var i = 0; i <= rightmostMovable; i++)
                positions[i] = false;

            // Set position at rightmostMovable - 1
            positions[rightmostMovable - 1] = true;

            // Set the remaining setBitsToRight positions at their highest consecutive positions
            // to the right of the moved position
            for (var i = 0; i < setBitsToRight; i++)
                positions[rightmostMovable - 2 - i] = true;

            // Convert back to array
            var finalResult = new int[k];
            var index = 0;
            for (var i = 0; i <= maxPos && index < k; i++)
                if (positions[i]) finalResult[index++] = i;

            return Create(finalResult);
        }


    }
}