namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative;

public sealed class GcSplit<T> : GenerativeCollection<T>
{
    public static GcSplit<T> Create(GenerativeCollection<T> upperCollection, GenerativeCollection<T> lowerCollection, int splitIndex)
    {
        return new GcSplit<T>(upperCollection, lowerCollection, splitIndex);
    }

    public static GcSplit<T> Create(GenerativeCollection<T> upperCollection, GenerativeCollection<T> lowerCollection)
    {
        return new GcSplit<T>(upperCollection, lowerCollection, 0);
    }


    public int SplitIndex { get; set; }

    public GenerativeCollection<T> UpperCollection { get; set; }

    public GenerativeCollection<T> LowerCollection { get; set; }

    public T this[int index]
    {
        get
        {
            if (index >= SplitIndex)
                return UpperCollection == null
                    ? DefaultValue
                    : UpperCollection.GetItem(index);

            return LowerCollection == null
                ? DefaultValue
                : LowerCollection.GetItem(index);
        }
    }


    private GcSplit(GenerativeCollection<T> upperCollection, GenerativeCollection<T> lowerCollection, int splitIndex)
    {
        UpperCollection = upperCollection;
        LowerCollection = lowerCollection;
        SplitIndex = splitIndex;
    }


    public override T GetItem(int index)
    {
        if (index >= SplitIndex)
            return UpperCollection == null 
                ? DefaultValue 
                : UpperCollection.GetItem(index);

        return LowerCollection == null
            ? DefaultValue
            : LowerCollection.GetItem(index);
    }
}