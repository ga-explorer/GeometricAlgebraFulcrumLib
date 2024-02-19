using System;

namespace DataStructuresLib.Collections.Generative.Finite.Natural;

/// <summary>
/// This class represents a part (slice) of a base collection
/// </summary>
/// <typeparam name="T"></typeparam>
public class NfcSlice<T> : NaturalFiniteCollection<T>
{
    public static NfcSlice<T> Create(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        return new NfcSlice<T>(baseCollection, firstIndex, lastIndex);
    }

    public static NfcSlice<T> CreateForwardSlice(FiniteCollection<T> baseCollection, int firstIndex)
    {
        return new NfcSlice<T>(baseCollection, firstIndex, baseCollection.MaxIndex);
    }

    public static NfcSlice<T> CreateReverseSlice(FiniteCollection<T> baseCollection, int firstIndex)
    {
        return new NfcSlice<T>(baseCollection, firstIndex, baseCollection.MinIndex);
    }

    public static NfcSlice<T> CreateFullForward(FiniteCollection<T> baseCollection)
    {
        return new NfcSlice<T>(baseCollection, baseCollection.MinIndex, baseCollection.MaxIndex);
    }

    public static NfcSlice<T> CreateFullReverse(FiniteCollection<T> baseCollection)
    {
        return new NfcSlice<T>(baseCollection, baseCollection.MaxIndex, baseCollection.MinIndex);
    }


    private int _valuesCount;


    public GenerativeCollection<T> BaseCollection { get; set; }

    public int FirstBaseIndex { get; private set; }

    public int LastBaseIndex { get; private set; }

    public bool IsReversed => LastBaseIndex < FirstBaseIndex;

    public override int Count => _valuesCount;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > MaxIndex || BaseCollection == null)
                return DefaultValue;

            index = IsReversed ? FirstBaseIndex - index : FirstBaseIndex + index;

            return BaseCollection.GetItem(index);
        }
    }


    private NfcSlice(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        BaseCollection = baseCollection;
        FirstBaseIndex = firstIndex;
        LastBaseIndex = lastIndex;

        _valuesCount = firstIndex <= lastIndex 
            ? lastIndex - firstIndex + 1 
            : firstIndex - lastIndex + 1;
    }


    public NfcSlice<T> Reset(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        BaseCollection = baseCollection;
        FirstBaseIndex = firstIndex;
        LastBaseIndex = lastIndex;

        _valuesCount = firstIndex <= lastIndex
            ? lastIndex - firstIndex + 1
            : firstIndex - lastIndex + 1;

        return this;
    }

    public override T GetItem(int index)
    {
        if (index < 0 || index > MaxIndex || BaseCollection == null)
            return DefaultValue;

        index = IsReversed ? FirstBaseIndex - index : FirstBaseIndex + index;

        return BaseCollection.GetItem(index);
    }
}