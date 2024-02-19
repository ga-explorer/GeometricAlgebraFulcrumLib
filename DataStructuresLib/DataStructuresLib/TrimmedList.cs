using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresLib;

/// <summary>
/// This is a wrapper for a generic List of type T but is capable of limiting item read access
/// between a start offset and an end offset. If start offset is > end offset the items are
/// referenced backwords.
/// </summary>
/// <typeparam name="T">The type of the items in the list</typeparam>
public sealed class TrimmedList<T>
{
    private readonly List<T> _internalList;


    public int ActiveStartOffset { get; private set; }

    public int ActiveLength { get; private set; }

    public int ActiveEndOffset => ActiveStartOffset + ActiveLength - 1;


    public int InternalCount => _internalList.Count;

    public List<T> InternalList => _internalList;

    public T FirstItem => _internalList[ActiveStartOffset];

    public T LastItem => _internalList[ActiveEndOffset];

    public T this[int index] => _internalList[GetInternalIndex(index)];


    public TrimmedList()
    {
        _internalList = new List<T>();
    }

    public TrimmedList(int capacity)
    {
        _internalList = new List<T>(capacity);
    }

    public TrimmedList(IEnumerable<T> collection)
    {
        _internalList = new List<T>(collection);
    }

    public TrimmedList(List<T> list)
    {
        _internalList = list;
    }

    public TrimmedList(TrimmedList<T> trimmedList)
    {
        _internalList = new List<T>(trimmedList._internalList);
        ActiveStartOffset = trimmedList.ActiveStartOffset;
        ActiveLength = trimmedList.ActiveLength;
    }


    public void SetActiveLength(int length)
    {
        var end = ActiveStartOffset + length - 1;

        if (length < 0 || end >= _internalList.Count)
            throw new IndexOutOfRangeException("Illegal active range for trimmed list");

        ActiveLength = length;
    }

    public void SetActiveRange(int start, int length)
    {
        var end = start + length - 1;

        if (length < 0 || start < 0 || end >= _internalList.Count)
            throw new IndexOutOfRangeException("Illegal active range for trimmed list");

        ActiveStartOffset = start;
        ActiveLength = length;
    }

    public void ResetActiveRange()
    {
        ActiveStartOffset = 0;
        ActiveLength = _internalList.Count;
    }



    public int GetInternalIndex(int index)
    {
        return ActiveStartOffset + index;
    }

    public List<T> GetActiveList()
    {
        return _internalList.GetRange(ActiveStartOffset, ActiveLength);
    }

    /// <summary>
    /// Increase ActiveStartOffset by 1 while keeping ActiveEndOffset constant.
    /// </summary>
    /// <returns>The new ActiveStartOffset</returns>
    public int IncreaseActiveStartOffset()
    {
        SetActiveRange(ActiveStartOffset + 1, ActiveLength - 1);

        return ActiveStartOffset;
    }

    /// <summary>
    /// Increase ActiveStartOffset by delta while keeping ActiveEndOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveStartOffset</returns>
    public int IncreaseActiveStartOffset(int delta)
    {
        SetActiveRange(ActiveStartOffset + delta, ActiveLength - delta);

        return ActiveStartOffset;
    }

    /// <summary>
    /// Increase ActiveLength by 1 while keeping ActiveStartOffset constant.
    /// </summary>
    /// <returns>The new ActiveLength</returns>
    public int IncreaseActiveLength()
    {
        SetActiveLength(ActiveLength + 1);

        return ActiveLength;
    }

    /// <summary>
    /// Increase ActiveLength by delta while keeping ActiveStartOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveLength</returns>
    public int IncreaseActiveLength(int delta)
    {
        SetActiveLength(ActiveLength + delta);

        return ActiveLength;
    }

    /// <summary>
    /// Increase ActiveEndOffset by 1 while keeping ActiveStartOffset constant.
    /// </summary>
    /// <returns>The new ActiveEndOffset</returns>
    public int IncreaseActiveEndOffset()
    {
        SetActiveLength(ActiveLength + 1);

        return ActiveEndOffset;
    }

    /// <summary>
    /// Increase ActiveEndOffset by delta while keeping ActiveStartOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveEndOffset</returns>
    public int IncreaseActiveEndOffset(int delta)
    {
        SetActiveLength(ActiveLength + delta);

        return ActiveEndOffset;
    }

    /// <summary>
    /// Decrease ActiveStartOffset by 1 while keeping ActiveEndOffset constant.
    /// </summary>
    /// <returns>The new ActiveStartOffset</returns>
    public int DecreaseActiveStartOffset()
    {
        SetActiveRange(ActiveStartOffset - 1, ActiveLength + 1);

        return ActiveStartOffset;
    }

    /// <summary>
    /// Decrease ActiveStartOffset by delta while keeping ActiveEndOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveStartOffset</returns>
    public int DecreaseActiveStartOffset(int delta)
    {
        SetActiveRange(ActiveStartOffset - delta, ActiveLength + delta);

        return ActiveStartOffset;
    }

    /// <summary>
    /// Decrease ActiveLength by 1 while keeping ActiveStartOffset constant.
    /// </summary>
    /// <returns>The new ActiveLength</returns>
    public int DecreaseActiveLength()
    {
        SetActiveLength(ActiveLength - 1);

        return ActiveLength;
    }

    /// <summary>
    /// Decrease ActiveLength by delta while keeping ActiveStartOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveLength</returns>
    public int DecreaseActiveLength(int delta)
    {
        SetActiveLength(ActiveLength - delta);

        return ActiveLength;
    }

    /// <summary>
    /// Decrease ActiveEndOffset by 1 while keeping ActiveStartOffset constant.
    /// </summary>
    /// <returns>The new ActiveEndOffset</returns>
    public int DecreaseActiveEndOffset()
    {
        SetActiveLength(ActiveLength - 1);

        return ActiveEndOffset;
    }

    /// <summary>
    /// Decrease ActiveEndOffset by delta while keeping ActiveStartOffset constant.
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>The new ActiveEndOffset</returns>
    public int DecreaseActiveEndOffset(int delta)
    {
        SetActiveLength(ActiveLength - delta);

        return ActiveEndOffset;
    }


    public void Add(T item)
    {
        _internalList.Add(item);
    }

    public void AddRange(IEnumerable<T> collection)
    {
        _internalList.AddRange(collection);
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        if (InternalList.Count <= 0) 
            return s.ToString();

        s.Append(InternalList[0]);

        for (var i = 1; i < InternalList.Count; i++)
        {
            s.Append(".");
            s.Append(InternalList[i]);
        }

        return s.ToString();
    }
}