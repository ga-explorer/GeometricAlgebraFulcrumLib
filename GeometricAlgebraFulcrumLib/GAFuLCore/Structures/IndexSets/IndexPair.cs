using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public readonly struct IndexPair :
    IPair<int>,
    IEquatable<IPair<int>>,
    IEquatable<IndexPair>,
    IComparable<IPair<int>>,
    IComparable<IndexPair>
{
    public int Index1 { get; }

    public int Index2 { get; }

    public int Item1
        => Index1;

    public int Item2
        => Index2;

    public bool IsSymmetric 
        => Index1 == Index2;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexPair(int index1)
    {
        Debug.Assert(index1 >= 0);

        Index1 = index1;
        Index2 = index1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexPair(int index1, int index2)
    {
        Debug.Assert(index1 >= 0 && index2 >= 0);

        Index1 = index1;
        Index2 = index2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexPair(IPair<int> pair)
    {
        Debug.Assert(pair.Item1 >= 0 && pair.Item2 >= 0);

        Index1 = pair.Item1;
        Index2 = pair.Item2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexPair Transpose()
    {
        return new IndexPair(Index2, Index1);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int index1, out int index2)
    {
        index1 = Index1;
        index2 = Index2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IndexPair other)
    {
        return Index1 == other.Index1 && Index2 == other.Index2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IPair<int> other)
    {
        if (ReferenceEquals(other, null)) return false;

        return Index1 == other.Item1 && Index2 == other.Item2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IndexPair other && Equals(other) ||
               obj is IPair<int> other1 && Equals(other1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Index1, Index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(IPair<int> other)
    {
        var item1Comparison = Index1.CompareTo(other.Item1);
        if (item1Comparison != 0) return item1Comparison;
        return Index2.CompareTo(other.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(IndexPair other)
    {
        var item1Comparison = Index1.CompareTo(other.Item1);
        if (item1Comparison != 0) return item1Comparison;
        return Index2.CompareTo(other.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"<{Index1}, {Index2}>";
    }
}