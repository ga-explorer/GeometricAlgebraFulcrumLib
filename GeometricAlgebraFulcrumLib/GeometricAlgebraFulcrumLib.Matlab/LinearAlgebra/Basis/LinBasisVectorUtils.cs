using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;

public static class LinBasisVectorUtils
{
    

    //
    //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, Scalar<T> scalar)
    //{
    //    return GaTermComposerUtils.ToTerm(basisBlade, scalar);
    //}

    //
    //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, IScalarProcessor<T> scalarProcessor, T scalarValue)
    //{
    //    return GaTermComposerUtils.ToTerm(basisBlade, scalarProcessor, scalarValue);
    //}

    //
    //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, IScalarProcessor<T> scalarProcessor)
    //{
    //    var scalar = term.Sign switch
    //    {
    //        0 => scalarProcessor.CreateScalarZero(),
    //        > 0 => scalarProcessor.CreateScalarOne(),
    //        _ => scalarProcessor.CreateScalarMinusOne()
    //    };

    //    return GaTermComposerUtils.ToTerm(term.BasisBlade, scalar);
    //}
    
    //
    //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, IScalarProcessor<T> scalarProcessor, T scalarValue)
    //{
    //    var scalar = term.Sign switch
    //    {
    //        0 => scalarProcessor.CreateScalarZero(),
    //        > 0 => scalarProcessor.CreateScalar(scalarValue),
    //        _ => scalarProcessor.CreateScalar(scalarProcessor.Negative(scalarValue))
    //    };

    //    return GaTermComposerUtils.ToTerm(term.BasisBlade, scalar);
    //}

    //
    //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, Scalar<T> scalar)
    //{
    //    return GaTermComposerUtils.ToTerm(term.BasisBlade, scalar * term.Sign);
    //}
    

    
    public static bool IsValidBasisVectorIndex(int basisVectorIndex)
    {
        return basisVectorIndex >= 0;
    }

    
    public static bool IsValidBasisBivectorIndices(int basisBivectorIndex1, int basisBivectorIndex2)
    {
        return basisBivectorIndex1 >= 0 && 
               basisBivectorIndex1 < basisBivectorIndex2;
    }
    
    public static bool IsValidBasisBladeIndexSet(IEnumerable<int> basisBladeIndexSet)
    {
        if (basisBladeIndexSet is IndexSet set)
            return set.IsValid();

        var i = -1;
        foreach (var index in basisBladeIndexSet)
        {
            if (i >= index)
                return false;

            i = index;
        }

        return true;
    }


    
    public static bool IsZeroVectorVectorOp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index == basisVector2Index;
    }
    
    
    public static bool IsZeroVectorBivectorOp(int basisVectorIndex, int basisBivectorIndex1, int basisBivectorIndex2)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
        );

        return basisVectorIndex == basisBivectorIndex1 ||
               basisVectorIndex == basisBivectorIndex2;
    }
    
    
    public static bool IsZeroVectorBladeOp(int basisVectorIndex, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisVectorIndex);
    }
    
    
    public static bool IsZeroBivectorVectorOp(int basisBivectorIndex1, int basisBivectorIndex2, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
        );

        return basisVectorIndex == basisBivectorIndex1 ||
               basisVectorIndex == basisBivectorIndex2;
    }
    
    
    public static bool IsZeroBivectorBivectorOp(int basisBivector1Index1, int basisBivector1Index2, int basisBivector2Index1, int basisBivector2Index2)
    {
        Debug.Assert(
            IsValidBasisBivectorIndices(basisBivector1Index1, basisBivector1Index2) &&
            IsValidBasisBivectorIndices(basisBivector2Index1, basisBivector2Index2)
        );

        return basisBivector1Index1 == basisBivector2Index1 ||
               basisBivector1Index1 == basisBivector2Index2 ||
               basisBivector1Index2 == basisBivector2Index1 ||
               basisBivector1Index2 == basisBivector2Index2;
    }

    
    public static bool IsZeroBivectorBladeOp(int basisBivectorIndex1, int basisBivectorIndex2, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
               basisBladeIndexSet.Contains(basisBivectorIndex2);
    }
    
    
    public static bool IsZeroBladeVectorOp(IndexSet basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisVectorIndex);
    }
    
    
    public static bool IsZeroBladeBivectorOp(IndexSet basisBladeIndexSet, int basisBivectorIndex1, int basisBivectorIndex2)
    {
        Debug.Assert(
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
               basisBladeIndexSet.Contains(basisBivectorIndex2);
    }

    
    public static bool IsZeroBladeBladeOp(IndexSet basisBlade1IndexSet, IndexSet basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade1IndexSet)
        );

        return basisBlade1IndexSet.SetOverlaps(basisBlade2IndexSet);
    }
    

    
    public static bool IsZeroVectorVectorESp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }
    
    
    public static bool IsZeroVectorBladeESp(int basisVectorIndex, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count != 1 || 
               basisBladeIndexSet.First() != basisVectorIndex;
    }

    
    public static bool IsZeroBladeVectorESp(IndexSet basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count != 1 || 
               basisBladeIndexSet.First() != basisVectorIndex;
    }
    
    
    public static bool IsZeroBladeBladeESp(IndexSet basisBlade1IndexSet, IndexSet basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade1IndexSet.Equals(basisBlade2IndexSet);
    }
    

    
    public static bool IsZeroVectorVectorELcp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }
    
    
    public static bool IsZeroVectorBladeELcp(int basisVectorIndex, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return !basisBladeIndexSet.Contains(basisVectorIndex);
    }

    
    public static bool IsZeroBladeVectorELcp(IndexSet basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count switch
        {
            0 => false,
            1 => basisBladeIndexSet.First() != basisVectorIndex,
            _ => true
        };
    }
    
    
    public static bool IsZeroBladeBladeELcp(IndexSet basisBlade1IndexSet, IndexSet basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade2IndexSet.SetIsSupersetOf(basisBlade1IndexSet);
    }
    

    
    public static bool IsZeroVectorVectorERcp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }

    
    public static bool IsZeroVectorBladeERcp(int basisVectorIndex, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count switch
        {
            0 => false,
            1 => basisBladeIndexSet.First() != basisVectorIndex,
            _ => true
        };
    }
    
    
    public static bool IsZeroBladeVectorERcp(IndexSet basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return !basisBladeIndexSet.Contains(basisVectorIndex);
    }

    
    public static bool IsZeroBladeBladeERcp(IndexSet basisBlade1IndexSet, IndexSet basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade1IndexSet.SetIsSupersetOf(basisBlade2IndexSet);
    }


    
    public static bool IsNegativeVectorVectorEGp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index > basisVector2Index;
    }
    
    
    public static bool IsNegativeVectorBladeEGp(int basisVectorIndex, IndexSet basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        if (basisBladeIndexSet.Count == 1)
            return basisVectorIndex > basisBladeIndexSet.First();

        return basisBladeIndexSet
            .TakeWhile(index2 => basisVectorIndex > index2)
            .Aggregate(false, (current, _) => !current);
    }
    
    
    public static bool IsNegativeBladeVectorEGp(IndexSet basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        if (basisBladeIndexSet.Count == 1)
            return basisBladeIndexSet.First() > basisVectorIndex;

        return basisBladeIndexSet
            .TakeWhile(index1 => index1 > basisVectorIndex)
            .Aggregate(false, (current, _) => !current);
    }

    
    public static bool IsNegativeBladeBladeEGp(IndexSet basisBlade1IndexSet, IndexSet basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        if (basisBlade1IndexSet.Count == 0)
            return false;

        if (basisBlade2IndexSet.Count == 0)
            return false;

        if (basisBlade1IndexSet.Count == 1)
            return IsNegativeVectorBladeEGp(basisBlade1IndexSet.First(), basisBlade2IndexSet);

        if (basisBlade2IndexSet.Count == 1)
            return IsNegativeBladeVectorEGp(basisBlade1IndexSet, basisBlade2IndexSet.First());

        return basisBlade1IndexSet
            .Reverse()
            .Aggregate(
                false, 
                (isNegative, index1) => isNegative ^ IsNegativeVectorBladeEGp(index1, basisBlade2IndexSet)
            );
    }

    
}