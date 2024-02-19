using System;
using System.Collections.Generic;

namespace DataStructuresLib.SubArray;

/// <inheritdoc />
/// <summary>
/// This class represents a sub-array of a 1D base array. The sub-array is specified by a first
/// and last index of the base array. The sub-array can be in reverse order by making the last index
/// less than the first index.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SubArray<T> : IReadOnlyList<T>
{
    /// <summary>
    /// Defines the order of the sub-array relative to the base array
    /// </summary>
    public bool IsReversed { get; set; }


    /// <summary>
    /// The base array of this sub-array
    /// </summary>
    public T[] BaseArray { get; }

    /// <summary>
    /// The first index of the base array correspondent to the zero index of this sub-array
    /// </summary>
    public int BaseArrayFirstIndex 
        => IsReversed 
            ? BaseArrayLargerIndex 
            : BaseArraySmallerIndex;

    /// <summary>
    /// The last index of the base array corresponding to the max index of the sub-array
    /// </summary>
    public int BaseArrayLastIndex 
        => IsReversed 
            ? BaseArraySmallerIndex 
            : BaseArrayLargerIndex;

    /// <summary>
    /// The smaller index of the base array. If the sub-array is in the reverse order this will equal
    /// BaseArrayLastIndex
    /// </summary>
    public int BaseArraySmallerIndex { get; private set; }

    /// <summary>
    /// The larger index of the base array. If the sub-array is in the reverse order this will equal
    /// BaseArrayFirstIndex
    /// </summary>
    public int BaseArrayLargerIndex { get; private set; }

    /// <summary>
    /// The number of items in this sub-array
    /// </summary>
    public int Count 
        => BaseArrayLargerIndex - BaseArraySmallerIndex + 1;

    /// <summary>
    /// The last index in this sub-array
    /// </summary>
    public int LastIndex 
        => BaseArrayLargerIndex - BaseArraySmallerIndex;

    /// <summary>
    /// Get or set an item in the sub-array
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return
                IsReversed 
                    ? BaseArray[BaseArrayLargerIndex - index]
                    : BaseArray[BaseArraySmallerIndex + index];
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (IsReversed)
                BaseArray[BaseArrayLargerIndex - index] = value;
            else
                BaseArray[BaseArraySmallerIndex + index] = value;
        }
    }

    /// <summary>
    /// The first item of this sub-array
    /// </summary>
    public T FirstItem 
        => Count > 0 ? BaseArray[BaseArrayFirstIndex] : default(T);

    /// <summary>
    /// The last item of this sub-array
    /// </summary>
    public T LastItem 
        => Count > 0 ? BaseArray[BaseArrayLastIndex] : default(T);


    /// <summary>
    /// This constructor defines the order of the sub-array implicitly from the baseFirstIndex 
    /// and baseLastIndex arguments
    /// </summary>
    /// <param name="baseArray"></param>
    /// <param name="baseFirstIndex"></param>
    /// <param name="baseLastIndex"></param>
    public SubArray(T[] baseArray, int baseFirstIndex, int baseLastIndex)
    {
        if (baseArray == null)
            throw new ArgumentNullException(nameof(baseArray));

        if (baseFirstIndex < 0 || baseFirstIndex >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseFirstIndex));

        if (baseLastIndex < 0 || baseLastIndex >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseLastIndex));

        BaseArray = baseArray;

        if (baseFirstIndex <= baseLastIndex)
        {
            IsReversed = false;
            BaseArraySmallerIndex = baseFirstIndex;
            BaseArrayLargerIndex = baseLastIndex;
        }
        else
        {
            IsReversed = true;
            BaseArraySmallerIndex = baseLastIndex;
            BaseArrayLargerIndex = baseFirstIndex;
        }
    }

    /// <summary>
    /// This constructor defines the order of the sub-array explicitly from the isReversed argument
    /// </summary>
    /// <param name="baseArray"></param>
    /// <param name="isReversed"></param>
    /// <param name="baseIndex1"></param>
    /// <param name="baseIndex2"></param>
    public SubArray(T[] baseArray, bool isReversed, int baseIndex1, int baseIndex2)
    {
        if (baseArray == null)
            throw new ArgumentNullException(nameof(baseArray));

        if (baseIndex1 < 0 || baseIndex1 >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseIndex1));

        if (baseIndex2 < 0 || baseIndex2 >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseIndex2));

        BaseArray = baseArray;
        IsReversed = isReversed;

        if (baseIndex1 < baseIndex2)
        {
            BaseArraySmallerIndex = baseIndex1;
            BaseArrayLargerIndex = baseIndex2;
        }
        else
        {
            BaseArraySmallerIndex = baseIndex2;
            BaseArrayLargerIndex = baseIndex1;
        }
    }


    /// <summary>
    /// Reset the first and last base array indices of this sub-array.
    /// The order is defined implicitly from the two arguments.
    /// If the two index arguments are equal the current order is preserved
    /// </summary>
    /// <param name="baseFirstIndex"></param>
    /// <param name="baseLastIndex"></param>
    /// <returns></returns>
    public SubArray<T> ResetBaseLimits(int baseFirstIndex, int baseLastIndex)
    {
        if (baseFirstIndex < 0 || baseFirstIndex >= BaseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseFirstIndex));

        if (baseLastIndex < 0 || baseLastIndex >= BaseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseLastIndex));

        if (baseFirstIndex < baseLastIndex)
        {
            IsReversed = false;
            BaseArraySmallerIndex = baseFirstIndex;
            BaseArrayLargerIndex = baseLastIndex;
        }
        else if (baseFirstIndex > baseLastIndex)
        {
            IsReversed = true;
            BaseArraySmallerIndex = baseLastIndex;
            BaseArrayLargerIndex = baseFirstIndex;
        }
        else
        {
            BaseArraySmallerIndex = baseFirstIndex;
            BaseArrayLargerIndex = baseLastIndex;
        }

        return this;
    }

    /// <summary>
    /// Reset the first and last base array indices of this sub-array
    /// The order is defined explicitly by the isReverse argument
    /// </summary>
    /// <param name="baseArray"></param>
    /// <param name="isReversed"></param>
    /// <param name="baseIndex1"></param>
    /// <param name="baseIndex2"></param>
    /// <returns></returns>
    public SubArray<T> ResetBaseLimits(T[] baseArray, bool isReversed, int baseIndex1, int baseIndex2)
    {
        if (baseIndex1 < 0 || baseIndex1 >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseIndex1));

        if (baseIndex2 < 0 || baseIndex2 >= baseArray.Length)
            throw new ArgumentOutOfRangeException(nameof(baseIndex1));

        IsReversed = isReversed;

        if (baseIndex1 < baseIndex2)
        {
            BaseArraySmallerIndex = baseIndex1;
            BaseArrayLargerIndex = baseIndex2;
        }
        else
        {
            BaseArraySmallerIndex = baseIndex2;
            BaseArrayLargerIndex = baseIndex1;
        }

        return this;
    }

    /// <summary>
    /// Get the base array index corresponding to the given sub-array index without performing
    /// any boundary checks
    /// </summary>
    /// <param name="subIndex"></param>
    /// <returns></returns>
    public int SubIndexToBaseIndex(int subIndex)
    {
        return 
            IsReversed 
                ? BaseArrayLargerIndex - subIndex
                : BaseArraySmallerIndex + subIndex;
    }

    /// <summary>
    /// Get the sub-array index corresponding to the given base array index without performing
    /// any boundary checks
    /// </summary>
    /// <param name="baseIndex"></param>
    /// <returns></returns>
    public int BaseIndexToSubIndex(int baseIndex)
    {
        return
            IsReversed
                ? BaseArrayLargerIndex - baseIndex
                : baseIndex - BaseArraySmallerIndex;
    }

    /// <summary>
    /// Given two indices of this sub-array this methods converts them into base array indices
    /// and create a new sub-array based on them
    /// </summary>
    /// <param name="firstIndex"></param>
    /// <param name="lastIndex"></param>
    /// <returns></returns>
    public SubArray<T> GetSubArray(int firstIndex, int lastIndex)
    {
        var baseFirstIndex = SubIndexToBaseIndex(firstIndex);
        var baseLastIndex = SubIndexToBaseIndex(lastIndex);

        return new SubArray<T>(BaseArray, baseFirstIndex, baseLastIndex);
    }

    /// <summary>
    /// Given two indices of this sub-array this methods converts them into base array indices
    /// and create a new sub-array based on them
    /// </summary>
    /// <param name="isReverse"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    public SubArray<T> GetSubArray(bool isReverse, int index1, int index2)
    {
        var baseIndex1 = SubIndexToBaseIndex(index1);
        var baseIndex2 = SubIndexToBaseIndex(index2);

        return new SubArray<T>(BaseArray, isReverse == IsReversed, baseIndex1, baseIndex2);
    }


    public IEnumerator<T> GetEnumerator()
    {
        if (IsReversed)
        {
            for (var i = BaseArrayLargerIndex; i >= BaseArraySmallerIndex; i--)
                yield return BaseArray[i];
        }
        else
        {
            for (var i = BaseArraySmallerIndex; i <= BaseArrayLargerIndex; i++)
                yield return BaseArray[i];
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        if (IsReversed)
        {
            for (var i = BaseArrayLargerIndex; i >= BaseArraySmallerIndex; i--)
                yield return BaseArray[i];
        }
        else
        {
            for (var i = BaseArraySmallerIndex; i <= BaseArrayLargerIndex; i++)
                yield return BaseArray[i];
        }
    }
}