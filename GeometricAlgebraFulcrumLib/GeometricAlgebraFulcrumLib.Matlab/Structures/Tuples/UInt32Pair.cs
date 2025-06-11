using System;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record UInt32Pair : 
    IPair<uint>,
    IComparable<UInt32Pair>
    {
    public uint Item1 { get; }

    public uint Item2 { get; }


    
    public UInt32Pair(uint item1, uint item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
    
    
    public UInt32Pair(IPair<uint> pair)
    {
        Item1 = pair.Item1;
        Item2 = pair.Item2;
    }

    
    
    public void Deconstruct(out uint item1, out uint item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
    
    
    public int CompareTo(UInt32Pair? other)
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