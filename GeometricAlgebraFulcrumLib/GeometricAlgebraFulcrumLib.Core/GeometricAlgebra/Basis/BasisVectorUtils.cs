using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;

public static class BasisVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisVectorIDs(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(IndexSet.CreateUnit);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> GetBasisVectorIndices(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(i => (ulong)i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BasisVectorIndexToMinVSpaceDimension(this ulong index)
    {
        return 1U + (uint) index;
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndexToId(this int index)
    {
        return IndexSet.CreateUnit(index);
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndexToId(this uint index)
    {
        return IndexSet.CreateUnit((int) index);
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndexToId(this ulong index)
    {
        return IndexSet.CreateUnit((int) index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong BasisVectorIdToIndex(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set
            ? (ulong) basisVectorId.FirstIndex
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisVectorId(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorId(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorId(this IndexSet basisVectorId, uint vSpaceDimensions)
    {
        return basisVectorId.IsUInt64Set &&
               basisVectorId.FirstIndex < vSpaceDimensions;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorIndex(this int index)
    {
        return index >= 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorIndex(this int basisVectorIndex, uint vSpaceDimensions)
    {
        return basisVectorIndex >= 0 && basisVectorIndex < vSpaceDimensions;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorIndex(this ulong basisVectorIndex, uint vSpaceDimensions)
    {
        return basisVectorIndex < vSpaceDimensions;
    }
}