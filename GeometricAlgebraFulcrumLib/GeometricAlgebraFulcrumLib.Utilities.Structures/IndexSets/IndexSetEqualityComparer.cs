﻿using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public sealed class IndexSetEqualityComparer : 
    IEqualityComparer<IndexSet>
{
    public static IndexSetEqualityComparer Instance { get; }
        = new IndexSetEqualityComparer();


    private IndexSetEqualityComparer()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IndexSet indexSet1, IndexSet indexSet2)
    {
        //if (ReferenceEquals(indexSet1, null) || ReferenceEquals(indexSet2, null))
        //    return false;

        //if (ReferenceEquals(indexSet1, indexSet2))
        //    return true;
            
        return indexSet1.Equals(indexSet2);

        //if (indexSet1.Count != indexSet2.Count)
        //    return false;

        //if (indexSet1 is UInt64IndexSet id1 && indexSet2 is UInt64IndexSet id2)
        //    return id1.IndexBitPattern == id2.IndexBitPattern;

        //using var enumerator1 = indexSet1.GetEnumerator();
        //using var enumerator2 = indexSet2.GetEnumerator();

        //while (enumerator1.MoveNext())
        //{
        //    if (!enumerator2.MoveNext() || enumerator1.Current != enumerator2.Current)
        //        return false;
        //}

        //return !enumerator2.MoveNext();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetHashCode(IndexSet indexSet)
    {
        return indexSet.GetHashCode();
    }
}