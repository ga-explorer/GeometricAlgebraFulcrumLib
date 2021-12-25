namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This structure represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Pair<T>(T Item1, T Item2) : IPair<T>;
    
    /// <summary>
    /// This structure represents an immutable triplet of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Triplet<T>(T Item1, T Item2, T Item3) : ITriplet<T>;
        
    /// <summary>
    /// This structure represents an immutable quad of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Quad<T>(T Item1, T Item2, T Item3, T Item4) : IQuad<T>;

    public sealed record Pair2D<T>(T Item11, T Item12, T Item21, T Item22) : IPair2D<T>;

}
