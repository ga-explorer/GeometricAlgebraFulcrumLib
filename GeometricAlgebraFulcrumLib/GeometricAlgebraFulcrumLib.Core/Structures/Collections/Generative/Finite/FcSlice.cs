namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite;

/// <summary>
/// This class represents a part (slice) of another base finite collection
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class FcSlice<T> : FiniteCollection<T>
{
    /// <summary>
    /// Create a slice of a base collection
    /// </summary>
    /// <param name="firstIndex"></param>
    /// <param name="lastIndex"></param>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public static FcSlice<T> Create(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        return new FcSlice<T>(baseCollection, firstIndex, lastIndex);
    }


    private int _firstBaseIndex;

    private int _lastBaseIndex;


    public GenerativeCollection<T> BaseCollection { get; set; }

    public override int MinIndex => _firstBaseIndex;

    public override int MaxIndex => _lastBaseIndex;

    public override int Count => _lastBaseIndex - _firstBaseIndex + 1;


    public T this[int index]
    {
        get
        {
            if (index < MinIndex || index > MaxIndex || BaseCollection == null)
                return DefaultValue;

            return BaseCollection.GetItem(index);
        }
    }


    private FcSlice(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        BaseCollection = baseCollection;

        if (firstIndex <= lastIndex)
        {
            _firstBaseIndex = firstIndex;
            _lastBaseIndex = lastIndex;
        }
        else
        {
            _firstBaseIndex = lastIndex;
            _lastBaseIndex = firstIndex;
        }
    }

    public override T GetItem(int index)
    {
        if (index < MinIndex || index > MaxIndex || BaseCollection == null)
            return DefaultValue;

        return BaseCollection.GetItem(index);
    }


    public FcSlice<T> Reset(GenerativeCollection<T> baseCollection, int firstIndex, int lastIndex)
    {
        BaseCollection = baseCollection;

        if (firstIndex <= lastIndex)
        {
            _firstBaseIndex = firstIndex;
            _lastBaseIndex = lastIndex;
        }
        else
        {
            _firstBaseIndex = lastIndex;
            _lastBaseIndex = firstIndex;
        }

        return this;
    }

    public FcSlice<T> ResetRange(int firstIndex, int lastIndex)
    {
        if (firstIndex <= lastIndex)
        {
            _firstBaseIndex = firstIndex;
            _lastBaseIndex = lastIndex;
        }
        else
        {
            _firstBaseIndex = lastIndex;
            _lastBaseIndex = firstIndex;
        }

        return this;
    }
}