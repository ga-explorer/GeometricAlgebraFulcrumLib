using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

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
            ? BitOperations.PopCount((value + 1).CreateMaskUInt64() & intArray) - 1
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
            ? BitOperations.PopCount((value + 1).CreateMaskUInt64() & intArray) - 1
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
            count.CreateMaskUInt64()
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
            count.CreateMaskUInt64() << firstIndex
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
                yield return new IndexSet(i);
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
        return IsUInt64Set && LastIndex < 31 
            ? (int)_bitPattern 
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ToUInt32()
    {
        return IsUInt64Set && LastIndex < 32 
            ? (uint)_bitPattern 
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long ToInt64()
    {
        return IsUInt64Set && LastIndex < 63 
            ? (long)_bitPattern 
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong ToUInt64()
    {
        return IsUInt64Set 
            ? _bitPattern 
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (bool found, ulong bitPattern) TryGetUInt64()
    {
        return IsUInt64Set 
            ? (true, _bitPattern)
            : (false, 0UL);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EInteger ToEInteger()
    {
        if (IsEmptySet)
            return EInteger.Zero;

        var two = EInteger.FromInt32(2);

        return _indexArray.Aggregate(
            EInteger.Zero, 
            (current, index) => current + two.Pow(index)
        );
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
        if (IsUnitSet) return (ulong) FirstIndex;
        
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
}