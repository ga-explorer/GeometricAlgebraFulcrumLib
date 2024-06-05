namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite.Natural;

/// <summary>
/// This class represents a periodic interleave of several collections. If an item
/// is not found in any collection the value is retrieved from a default base collection
/// that also defines the total count of the items in the interleaved collection
/// </summary>
/// <typeparam name="T"></typeparam>
public class NfcInterleaved<T> : NaturalFiniteCollection<T>
{
    public static NfcInterleaved<T> Create(FiniteCollection<T> defaultBaseCollection, params FiniteCollection<T>[] interleavedCollections)
    {
        return new NfcInterleaved<T>(defaultBaseCollection, interleavedCollections);
    }

    public static NfcInterleaved<T> Create(FiniteCollection<T> defaultBaseCollection, IEnumerable<FiniteCollection<T>> interleavedCollections)
    {
        return new NfcInterleaved<T>(defaultBaseCollection, interleavedCollections.ToArray());
    }

    public static NfcInterleaved<T> Create(int itemsCount, T defaultValue, params FiniteCollection<T>[] interleavedCollections)
    {
        return new NfcInterleaved<T>(
            NfcConstant<T>.Create(itemsCount, defaultValue), 
            interleavedCollections
        );
    }

    public static NfcInterleaved<T> Create(int itemsCount, T defaultValue, IEnumerable<FiniteCollection<T>> interleavedCollections)
    {
        return new NfcInterleaved<T>(
            NfcConstant<T>.Create(itemsCount, defaultValue),
            interleavedCollections.ToArray()
        );
    }


    public FiniteCollection<T>[] InterleavedCollections { get; private set; }

    public FiniteCollection<T> DefaultBaseCollection { get; private set; }

    public override int Count => DefaultBaseCollection.Count;

    public T this[int index]
    {
        get
        {
            var itemIndex = index / InterleavedCollections.Length;
            var baseCol = InterleavedCollections[index % InterleavedCollections.Length];

            return baseCol != null && itemIndex < baseCol.Count
                ? baseCol.GetItem(baseCol.MinIndex + itemIndex)
                : DefaultBaseCollection.GetItem(DefaultBaseCollection.MinIndex + index);
        }
    }


    private NfcInterleaved(FiniteCollection<T> defaultBaseCollection, params FiniteCollection<T>[] interleavedCollections)
    {
        InterleavedCollections = interleavedCollections;
        DefaultBaseCollection = defaultBaseCollection;
    }


    public override T GetItem(int index)
    {
        var itemIndex = index / InterleavedCollections.Length;
        var baseCol = InterleavedCollections[index % InterleavedCollections.Length];

        return baseCol != null && itemIndex < baseCol.Count
            ? baseCol.GetItem(baseCol.MinIndex + itemIndex)
            : DefaultBaseCollection.GetItem(DefaultBaseCollection.MinIndex + index);
    }


    public NfcInterleaved<T> Reset(FiniteCollection<T> defaultBaseCollection, params FiniteCollection<T>[] interleavedCollections)
    {
        DefaultBaseCollection = defaultBaseCollection;
        InterleavedCollections = interleavedCollections;

        return this;
    }

    public NfcInterleaved<T> Reset(FiniteCollection<T> defaultBaseCollection, IEnumerable<FiniteCollection<T>> interleavedCollections)
    {
        DefaultBaseCollection = defaultBaseCollection;
        InterleavedCollections = interleavedCollections.ToArray();

        return this;
    }

    public NfcInterleaved<T> Reset(int itemsCount, T defaultValue, params FiniteCollection<T>[] interleavedCollections)
    {
        DefaultBaseCollection = NfcConstant<T>.Create(itemsCount, defaultValue);
        InterleavedCollections = interleavedCollections;

        return this;
    }

    public NfcInterleaved<T> Reset(int itemsCount, T defaultValue, IEnumerable<FiniteCollection<T>> interleavedCollections)
    {
        DefaultBaseCollection = NfcConstant<T>.Create(itemsCount, defaultValue);
        InterleavedCollections = interleavedCollections.ToArray();

        return this;
    }

    public NfcInterleaved<T> ResetDefault(FiniteCollection<T> defaultBaseCollection)
    {
        DefaultBaseCollection = defaultBaseCollection;

        return this;
    }

    public NfcInterleaved<T> ResetDefault(int itemsCount, T defaultValue)
    {
        DefaultBaseCollection = NfcConstant<T>.Create(itemsCount, defaultValue);

        return this;
    }

    public NfcInterleaved<T> ResetInterleaved(params FiniteCollection<T>[] interleavedCollections)
    {
        InterleavedCollections = interleavedCollections;

        return this;
    }

    public NfcInterleaved<T> ResetInterleaved(IEnumerable<FiniteCollection<T>> interleavedCollections)
    {
        InterleavedCollections = interleavedCollections.ToArray();

        return this;
    }
}