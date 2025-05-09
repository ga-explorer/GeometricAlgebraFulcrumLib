namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

/// <summary>
/// This structure represents an immutable triplet of items of the same type
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record Triplet<T> : 
    ITriplet<T>
{
    public T Item1 { get; }

    public T Item2 { get; }

    public T Item3 { get; }


    /// <summary>
    /// This structure represents an immutable triplet of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public Triplet(T item1, T item2, T item3)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }


    public void Deconstruct(out T item1, out T item2, out T item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
    }
}