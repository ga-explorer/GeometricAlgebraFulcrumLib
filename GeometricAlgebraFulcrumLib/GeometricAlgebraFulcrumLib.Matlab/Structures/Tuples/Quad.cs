namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    /// <summary>
    /// This structure represents an immutable quad of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Quad<T> : 
    IQuad<T>
    {
    public T Item1 { get; }

    public T Item2 { get; }

    public T Item3 { get; }

    public T Item4 { get; }


    /// <summary>
    /// This structure represents an immutable quad of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public Quad(T item1, T item2, T item3, T item4)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
    }


    public void Deconstruct(out T item1, out T item2, out T item3, out T item4)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
    }
    }
}