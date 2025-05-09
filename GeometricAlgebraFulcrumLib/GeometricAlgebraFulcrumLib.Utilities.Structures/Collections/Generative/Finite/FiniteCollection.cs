using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite.Iterators;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite.Natural;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite;

/// <summary>
/// The base class for finite collections
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class FiniteCollection<T> : GenerativeCollection<T>, IFiniteCollection<T>
{
    public abstract int MinIndex { get; }

    public abstract int MaxIndex { get; }

    public abstract int Count { get; }


    /// <summary>
    /// Returns true if the given index is within the legal range for this collection
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool IsLegalIndex(int index)
    {
        return index >= MinIndex && index <= MaxIndex;
    }

    /// <summary>
    /// Raises an exception if the given index is not in the legal range
    /// </summary>
    /// <param name="index"></param>
    public void ValidateIndex(int index)
    {
        if (index < MinIndex || index > MaxIndex)
            throw new IndexOutOfRangeException();
    }

    public T GetItemByOffset(int offset)
    {
        return GetItem(MinIndex + offset.Mod(Count));
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
        return new FiniteCollectionEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Get a sequence of key-value pairs where the key is an index and the value is
    /// an element of this collection
    /// </summary>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<int, T>> GetKeyValuePairs()
    {
        var index = MinIndex;

        return this.Select(v => new KeyValuePair<int, T>(index++, v));
    }

    /// <summary>
    /// Convert this collection into a dictionary
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, T> GetDictionary()
    {
        var index = MinIndex;

        return this.ToDictionary(value => index++);
    }

    /// <summary>
    /// Perform an index-shift operation on the elements of this collection
    /// </summary>
    /// <param name="shiftCount"></param>
    /// <returns></returns>
    public new FcShiftReflect<T> ShiftBy(int shiftCount)
    {
        var srCol = this as FcShiftReflect<T> ?? FcShiftReflect<T>.Create(this);

        return srCol.ApplyShift(shiftCount);
    }

    /// <summary>
    /// Perform an index-reflection operation on the elements of this collection
    /// </summary>
    /// <param name="reflectionCenter"></param>
    /// <returns></returns>
    public new FcShiftReflect<T> ReflectOn(int reflectionCenter)
    {
        var srCol = this as FcShiftReflect<T> 
                    ?? FcShiftReflect<T>.Create(this);

        return srCol.ApplyReflect(reflectionCenter);
    }

    /// <summary>
    /// Convert this collection into a natural collection
    /// </summary>
    /// <returns></returns>
    public NaturalFiniteCollection<T> ToNaturalCollection()
    {
        return this as NaturalFiniteCollection<T> 
               ?? NfcSlice<T>.CreateFullForward(this);
    }


    public FciSliceIterator<T> GetSliceIterator()
    {
        return new FciSliceIterator<T>(this, MinIndex, MaxIndex);
    }

    public FciSliceIterator<T> GetSliceIterator(bool goForward)
    {
        return 
            goForward
                ? new FciSliceIterator<T>(this, MinIndex, MaxIndex)
                : new FciSliceIterator<T>(this, MaxIndex, MinIndex);
    }

    public FciSliceIterator<T> GetSliceIterator(int startIndex)
    {
        ValidateIndex(startIndex);

        return new FciSliceIterator<T>(this, startIndex, MaxIndex);
    }

    public FciSliceIterator<T> GetSliceIterator(int startIndex, bool goForward)
    {
        ValidateIndex(startIndex);

        return
            goForward
                ? new FciSliceIterator<T>(this, startIndex, MaxIndex)
                : new FciSliceIterator<T>(this, startIndex, MinIndex);
    }

    public FciPeriodicIterator<T> GetPeriodicIterator()
    {
        return new FciPeriodicIterator<T>(this);
    }

    public FciPeriodicIterator<T> GetPeriodicIterator(bool goForward)
    {
        return
            goForward
                ? new FciPeriodicIterator<T>(this, MinIndex, MaxIndex)
                : new FciPeriodicIterator<T>(this, MaxIndex, MinIndex);
    }

    public FciPeriodicIterator<T> GetPeriodicIterator(int startIndex)
    {
        ValidateIndex(startIndex);

        return new FciPeriodicIterator<T>(this, startIndex, MaxIndex);
    }

    public FciPeriodicIterator<T> GetPeriodicIterator(int startIndex, bool goForward)
    {
        ValidateIndex(startIndex);

        return
            goForward
                ? new FciPeriodicIterator<T>(this, startIndex, MaxIndex)
                : new FciPeriodicIterator<T>(this, startIndex, MinIndex);
    }

    public FciSwingIterator<T> GetSwingIterator()
    {
        return new FciSwingIterator<T>(this);
    }

    public FciSwingIterator<T> GetSwingIterator(bool goForward)
    {
        return
            goForward
                ? new FciSwingIterator<T>(this, MinIndex, MaxIndex)
                : new FciSwingIterator<T>(this, MaxIndex, MinIndex);
    }

    public FciSwingIterator<T> GetSwingIterator(int startIndex)
    {
        ValidateIndex(startIndex);

        return new FciSwingIterator<T>(this, startIndex, MaxIndex);
    }

    public FciSwingIterator<T> GetSwingIterator(int startIndex, bool goForward)
    {
        ValidateIndex(startIndex);

        return
            goForward
                ? new FciSwingIterator<T>(this, startIndex, MaxIndex)
                : new FciSwingIterator<T>(this, startIndex, MinIndex);
    }
}