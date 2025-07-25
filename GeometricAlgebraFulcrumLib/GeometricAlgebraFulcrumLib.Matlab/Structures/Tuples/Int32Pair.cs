﻿using System;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record Int32Pair : 
    IPair<int>,
    IComparable<Int32Pair>
    {
    public int Item1 { get; }

    public int Item2 { get; }


    
    public Int32Pair(int item1, int item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
    
    
    public Int32Pair(IPair<int> pair)
    {
        Item1 = pair.Item1;
        Item2 = pair.Item2;
    }

    
    
    public void Deconstruct(out int item1, out int item2)
    {
        item1 = Item1;
        item2 = Item2;
    }

    
    public int CompareTo(Int32Pair other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var item1Comparison = Item1.CompareTo(other.Item1);
        if (item1Comparison != 0) return item1Comparison;
        return Item2.CompareTo(other.Item2);
    }

    
    public override string ToString()
    {
        return $"({Item1}, {Item2})";
    }
    }
}