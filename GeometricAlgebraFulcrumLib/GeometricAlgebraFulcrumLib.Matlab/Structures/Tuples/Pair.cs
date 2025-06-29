﻿namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    /// <summary>
    /// This structure represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed record Pair<T> : 
    IPair<T>
    {
    public T Item1 { get; }

    public T Item2 { get; }


    /// <summary>
    /// This structure represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public Pair(T item1, T item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
    
    
    public Pair(IPair<T> pair)
    {
        Item1 = pair.Item1;
        Item2 = pair.Item2;
    }


    
    public void Deconstruct(out T item1, out T item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
    }
}