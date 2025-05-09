using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public interface IIndexSet : 
    IReadOnlyList<int>, 
    IEquatable<IIndexSet>,
    IComparable<IIndexSet>
{
    /// <summary>
    /// True if this index set is empty
    /// </summary>
    bool IsEmptySet { get; }

    /// <summary>
    /// True if this index set contains a single index only
    /// </summary>
    bool IsSingleIndexSet { get; }

    /// <summary>
    /// True if this set contains two indices only
    /// </summary>
    bool IsIndexPairSet { get; }

    /// <summary>
    /// True if this set is not empty and sparse (there are gaps within the indices)
    /// </summary>
    bool IsSparseSet { get; }
        
    /// <summary>
    /// True if this set is not empty and dense (there are no gaps within the indices)
    /// </summary>
    bool IsDenseSet { get; }

    /// <summary>
    /// True if this set is empty or the largest index in this set is less than 64
    /// </summary>
    bool IsUInt64Set { get; }
    
    /// <summary>
    /// The smallest index in this set
    /// </summary>
    int FirstIndex { get; }

    /// <summary>
    /// The largest index in this set
    /// </summary>
    int LastIndex { get; }

    /// <summary>
    /// The range of indices in this set
    /// </summary>
    /// <returns></returns>
    Pair<int> GetIndexRange();

    /// <summary>
    /// The indices of this set in descending order
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> GetReversedIndices();

    /// <summary>
    /// Add an index to this set
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IIndexSet Add(int index);

    /// <summary>
    /// Remove an index from this set
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IIndexSet Remove(int index);

    /// <summary>
    /// Remove an index from this set if present
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IIndexSet RemoveIfContains(int index);

    /// <summary>
    /// True if this set contains the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    bool Contains(int index);

    bool ContainsSingleIndex(int index);

    bool Contains(ulong indexSet);

    bool Contains(IIndexSet indexSet);

    bool Overlaps(ulong indexSet);

    bool Overlaps(IIndexSet indexSet);

    IIndexSet MapIndices(Func<int, int> indexMapping);

    IIndexSet ShiftIndices(int offset);

    IIndexSet Intersect(ulong indexSet);

    IIndexSet Intersect(IIndexSet indexSet);

    IIndexSet Join(ulong indexSet);

    IIndexSet Join(IIndexSet indexSet);

    IIndexSet Difference(ulong indexSet);

    IIndexSet Difference(IIndexSet indexSet);

    IIndexSet SymmetricDifference(ulong indexSet);

    IIndexSet SymmetricDifference(IIndexSet indexSet);

    SparseIndexSet ToSparseIndexSet();
        
    DenseIndexSet ToDenseIndexSet();

    UInt64IndexSet ToUInt64IndexSet();

    bool TryGetUInt64BitPattern(out ulong bitPattern);
}