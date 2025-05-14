using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

public static class LinBasisVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector ToSignedBasisVector(this LinBasisVector basisBlade, IntegerSign sign)
    {
        return new LinSignedBasisVector(basisBlade, sign);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, Scalar<T> scalar)
    //{
    //    return GaTermComposerUtils.ToTerm(basisBlade, scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, IScalarProcessor<T> scalarProcessor, T scalarValue)
    //{
    //    return GaTermComposerUtils.ToTerm(basisBlade, scalarProcessor, scalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, Scalar<T> scalar)
    //{
    //    return GaTermComposerUtils.ToTerm(term.BasisBlade, scalar * term.Sign);
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorTerm ToTermFloat64(this LinSignedBasisVector term, double scalar)
    {
        return term.Index.CreateLinTerm(scalar * term.Sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisVectorIndex(int basisVectorIndex)
    {
        return basisVectorIndex >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorVectorOp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index == basisVector2Index;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorBivectorOp(int basisVectorIndex, int basisBivectorIndex1, int basisBivectorIndex2)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
        );

        return basisVectorIndex == basisBivectorIndex1 ||
               basisVectorIndex == basisBivectorIndex2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorBladeOp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisVectorIndex);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBivectorVectorOp(int basisBivectorIndex1, int basisBivectorIndex2, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
        );

        return basisVectorIndex == basisBivectorIndex1 ||
               basisVectorIndex == basisBivectorIndex2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBivectorBladeOp(int basisBivectorIndex1, int basisBivectorIndex2, IReadOnlySet<int> basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
               basisBladeIndexSet.Contains(basisBivectorIndex2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeVectorOp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisVectorIndex);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeBivectorOp(IReadOnlySet<int> basisBladeIndexSet, int basisBivectorIndex1, int basisBivectorIndex2)
    {
        Debug.Assert(
            IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
               basisBladeIndexSet.Contains(basisBivectorIndex2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeBladeOp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade1IndexSet)
        );

        return basisBlade1IndexSet.Overlaps(basisBlade2IndexSet);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorVectorESp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorBladeESp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count != 1 || 
               basisBladeIndexSet.First() != basisVectorIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeVectorESp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return basisBladeIndexSet.Count != 1 || 
               basisBladeIndexSet.First() != basisVectorIndex;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeBladeESp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade1IndexSet.SetEquals(basisBlade2IndexSet);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorVectorELcp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorBladeELcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return !basisBladeIndexSet.Contains(basisVectorIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeVectorELcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeBladeELcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade1IndexSet.IsSubsetOf(basisBlade2IndexSet);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorVectorERcp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index != basisVector2Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroVectorBladeERcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeVectorERcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVectorIndex) &&
            IsValidBasisBladeIndexSet(basisBladeIndexSet)
        );

        return !basisBladeIndexSet.Contains(basisVectorIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroBladeBladeERcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
    {
        Debug.Assert(
            IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
            IsValidBasisBladeIndexSet(basisBlade2IndexSet)
        );

        return !basisBlade1IndexSet.IsSubsetOf(basisBlade2IndexSet);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeVectorVectorEGp(int basisVector1Index, int basisVector2Index)
    {
        Debug.Assert(
            IsValidBasisVectorIndex(basisVector1Index) &&
            IsValidBasisVectorIndex(basisVector2Index)
        );

        return basisVector1Index > basisVector2Index;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeVectorBladeEGp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeBladeVectorEGp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeBladeBladeEGp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToTuple(this ILinSignedBasisVector basisVector, int dimensions)
    {
        return LinFloat64Vector.CreateScaledBasis(
            dimensions,
            basisVector.Index,
            basisVector.IsNegative ? -1d : 1d
        );
    }

}