using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

/// <summary>
/// This class include some useful general utilities for computing Combinations
/// of the form C(n, 2)
/// </summary>
public static class BasisBivectorUtils
{
    
    public static IEnumerable<IndexSet> GetBasisBivectorIds(this int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var i1 = 0; i1 < vSpaceDimensions - 1; i1++)
        for (var i2 = i1 + 1; i2 < vSpaceDimensions; i2++)
            yield return IndexSet.CreatePair(i1, i2);
    }

    
    public static IEnumerable<ulong> GetBasisBivectorIndices(this int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        return Enumerable.Range(0, vSpaceDimensions * (vSpaceDimensions - 1)).Select(i => (ulong) i);
    }
        
    
    public static IEnumerable<Pair<ulong>> GetBasisBivectorVectorIndices(this int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var i1 = 0; i1 < vSpaceDimensions - 1; i1++)
        for (var i2 = i1 + 1; i2 < vSpaceDimensions; i2++)
            yield return new Pair<ulong>((ulong)i1, (ulong)i2);
    }

    
    public static int BasisBivectorIndexToMinVSpaceDimension(this ulong index)
    {
        return 1 + (int) (0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
    }

    
    public static bool IsBasisBivectorId(this IndexSet basisBladeId)
    {
        return basisBladeId.IsPairSet;
    }
    
    
    public static IndexSet IndexPairToBivectorId(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        Debug.Assert(index1 >= 0 && index2 >= 0 && index1 != index2);

        return IndexSet.CreatePair(index1, index2);
    }

    
    public static IndexSet IndexPairToBivectorId(int index1, int index2)
    {
        Debug.Assert(index1 >= 0 && index2 >= 0 && index1 != index2);

        return IndexSet.CreatePair(index1, index2);
    }

    
    public static IndexSet IndexPairToBivectorId(ulong index1, ulong index2)
    {
        Debug.Assert(index1 != index2);

        return IndexSet.CreatePair((int) index1, (int) index2);
    }
    
    
    public static Pair<ulong> BasisBivectorIndexToVectorIndices(this ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return new Pair<ulong>(n1, n2);
    }
        
    
    public static ulong BasisBivectorIndexToVectorIndex1(this ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return n1;
    }
        
    
    public static ulong BasisBivectorIndexToVectorIndex2(this ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        //var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return n2;
    }
        
    
    public static IndexSet BasisBivectorIndexToId(this int index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * (ulong)index)));
        var n1 = (ulong)index - ((n2 * (n2 - 1UL)) >> 1);

        return IndexSet.CreatePair((int) n1, (int) n2);
    }

    
    public static IndexSet BasisBivectorIndexToId(this ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return IndexSet.CreatePair((int) n1, (int) n2);
    }

    
    public static ulong BasisVectorIndicesToBivectorIndex(this Pair<ulong> basisVectorIndexPair)
    {
        var (n1, n2) = basisVectorIndexPair;

        Debug.Assert(n1 != n2);

        return n1 < n2
            ? n1 + ((n2 * (n2 - 1UL)) >> 1)
            : n2 + ((n1 * (n1 - 1UL)) >> 1);
    }

    
    public static ulong BasisVectorIndicesToBivectorIndex(int index1, int index2)
    {
        Debug.Assert(index1 >= 0 && index2 >= 0 && index1 != index2);

        var n1 = (ulong) index1;
        var n2 = (ulong) index2;

        return n1 < n2
            ? n1 + ((n2 * (n2 - 1UL)) >> 1)
            : n2 + ((n1 * (n1 - 1UL)) >> 1);
    }

    
    public static ulong BasisVectorIndicesToBivectorIndex(ulong index1, ulong index2)
    {
        Debug.Assert(index1 != index2);

        return index1 < index2
            ? index1 + ((index2 * (index2 - 1UL)) >> 1)
            : index2 + ((index1 * (index1 - 1UL)) >> 1);
    }

    
    public static ulong BasisBivectorIdToIndex(this IndexSet basisBladeId)
    {
        //var n2 = (ulong) Math.Log(basisBladeId, 2);
        //var n1 = (ulong) Math.Log(basisBladeId - (1UL << (int)n2), 2);
            
        //return n1 + ((n2 * (n2 - 1UL)) >> 1);
        
        return basisBladeId.DecodeCombinadicToUInt64();
    }


        
    
    public static bool IsValidBasisBivectorId(this IndexSet basisBivectorId)
    {
        return basisBivectorId.IsPairSet;
    }
        
    
    public static bool IsValidBasisBivectorId(this IndexSet basisBivectorId, uint vSpaceDimensions)
    {
        return basisBivectorId.IsPairSet &&
               basisBivectorId.LastIndex < (int)vSpaceDimensions;
    }
    
    
    public static bool IsValidBasisBivectorIndex(ulong basisBivectorIndex, uint vSpaceDimensions)
    {
        var kvDim = vSpaceDimensions.GetBinomialCoefficient(2);

        return basisBivectorIndex < kvDim;
    }
}