using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

public static class BasisVectorUtils
{
    
    public static IEnumerable<IndexSet> GetBasisVectorIDs(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(IndexSet.CreateUnit);
    }

    
    public static IEnumerable<ulong> GetBasisVectorIndices(this int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(i => (ulong)i);
    }

    
    public static uint BasisVectorIndexToMinVSpaceDimension(this ulong index)
    {
        return 1U + (uint) index;
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    
    public static IndexSet BasisVectorIndexToId(this int index)
    {
        return IndexSet.CreateUnit(index);
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    
    public static IndexSet BasisVectorIndexToId(this uint index)
    {
        return IndexSet.CreateUnit((int) index);
    }

    /// <summary>
    /// Find the ID of a basis vector given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    
    public static IndexSet BasisVectorIndexToId(this ulong index)
    {
        return IndexSet.CreateUnit((int) index);
    }
    
    
    public static ulong BasisVectorIdToIndex(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set
            ? (ulong) basisVectorId.FirstIndex
            : throw new InvalidOperationException();
    }

    
    public static bool IsBasisVectorId(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set;
    }

    
    public static bool IsValidBasisVectorId(this IndexSet basisVectorId)
    {
        return basisVectorId.IsUInt64Set;
    }
        
    
    public static bool IsValidBasisVectorId(this IndexSet basisVectorId, uint vSpaceDimensions)
    {
        return basisVectorId.IsUInt64Set &&
               basisVectorId.FirstIndex < vSpaceDimensions;
    }
        
    
    public static bool IsValidBasisVectorIndex(this int index)
    {
        return index >= 0;
    }
    
    
    public static bool IsValidBasisVectorIndex(this int basisVectorIndex, uint vSpaceDimensions)
    {
        return basisVectorIndex >= 0 && basisVectorIndex < vSpaceDimensions;
    }
    
    
    public static bool IsValidBasisVectorIndex(this ulong basisVectorIndex, uint vSpaceDimensions)
    {
        return basisVectorIndex < vSpaceDimensions;
    }
}