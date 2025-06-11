using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

public static class BasisBladeProductUtils
{
    /// <summary>
    /// True if the outer product of the given euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return !id1.SetOverlaps(id2);
    }

    /// <summary>
    /// True if the outer product of the given euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="id3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OpIsNonZero(IndexSet id1, IndexSet id2, IndexSet id3)
    {
        return !id1.SetOverlaps(id2) && 
               !id2.SetOverlaps(id3) &&
               !id3.SetOverlaps(id1);
    }

    /// <summary>
    /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return true;
    }

    /// <summary>
    /// True if the scalar product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ESpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return id1.Equals(id2);
    }
    
    /// <summary>
    /// True if the left contraction product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ELcpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return id2.SetIsSupersetOf(id1);
    }

    /// <summary>
    /// True if the right contraction product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ERcpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return id1.SetIsSupersetOf(id2);
    }
    
    /// <summary>
    /// True if the fat-dot product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EFdpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return id1.SetIsSupersetOf(id2) || 
               id2.SetIsSupersetOf(id1);
    }

    /// <summary>
    /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EHipIsNonZero(this IndexSet id1, IndexSet id2)
    {
        return !id1.IsEmptySet && 
               !id2.IsEmptySet && 
               (id1.SetIsSupersetOf(id2) || id2.SetIsSupersetOf(id1));
    }

    /// <summary>
    /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EAcpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        //A acp B = (AB + BA) / 2
        return EGpIsNegative(id1, id2) == EGpIsNegative(id2, id1);
    }

    /// <summary>
    /// True if the commutator product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ECpIsNonZero(this IndexSet id1, IndexSet id2)
    {
        //A cp B = (AB - BA) / 2
        return EGpIsNegative(id1, id2) != EGpIsNegative(id2, id1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroTriProductLeftAssociative(IndexSet id1, IndexSet id2, IndexSet id3, Func<IndexSet, IndexSet, bool> isNonZeroProductFunc1, Func<IndexSet, IndexSet, bool> isNonZeroProductFunc2)
    {
        return isNonZeroProductFunc1(id1, id2) && isNonZeroProductFunc2(id1 ^ id2, id3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroTriProductRightAssociative(IndexSet id1, IndexSet id2, IndexSet id3, Func<IndexSet, IndexSet, bool> isNonZeroProductFunc1, Func<IndexSet, IndexSet, bool> isNonZeroProductFunc2)
    {
        return isNonZeroProductFunc1(id1, id2 ^ id3) && isNonZeroProductFunc2(id2, id3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroELcpELcpLa(IndexSet id1, IndexSet id2, IndexSet id3)
    {
        return IsNonZeroTriProductLeftAssociative(id1, id2, id3, ELcpIsNonZero, ELcpIsNonZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroELcpELcpRa(IndexSet id1, IndexSet id2, IndexSet id3)
    {
        return IsNonZeroTriProductRightAssociative(id1, id2, id3, ELcpIsNonZero, ELcpIsNonZero);
    }


    /// <summary>
    /// Given a bit pattern in id1 and id2 this shifts id2 by MaxVSpaceDimension bits to the left and 
    /// appends id1 to combine the two patterns using an OR bitwise operation
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet JoinIDs(IndexSet id1, IndexSet id2)
    {
        return id1 | (id2 << (int) BasisBladeDataLookup.MaxVSpaceDimension);
    }

    /// <summary>
    /// Given a bit pattern in id1 and id2 this shifts id2 by VSpaceDim bits to the left and 
    /// appends id1 to combine the two patterns using an OR bitwise operation
    /// </summary>
    /// <param name="vSpaceDimensions"></param>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet JoinIDs(uint vSpaceDimensions, IndexSet id1, IndexSet id2)
    {
        return id1 | (id2 << (int) vSpaceDimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSquaredSign(this IndexSet id)
    {
        return id.SetCountSwapsWithSelf().IsEven()
            ? IntegerSign.Positive 
            : IntegerSign.Negative;
    }
    
    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsPositive(this IndexSet id1, IndexSet id2)
    {
        return id1.SetCountSwaps(id2).IsEven();
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsNegative(this IndexSet id1, IndexSet id2)
    {
        return id1.SetCountSwaps(id2).IsOdd();
    }
    
    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpSquaredIsPositive(this IndexSet id)
    {
        return id.SetCountSwapsWithSelf().IsEven();
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of a basis blade with itself is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id1"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpSquaredIsNegative(this IndexSet id1)
    {
        return id1.SetCountSwapsWithSelf().IsOdd();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, IndexSet> EGpIsPositiveId(this IndexSet id1, IndexSet id2)
    {
        var (swapCount, indexSet) = id1.SetMergeCountSwaps(id2);

        return new Tuple<bool, IndexSet>(
            swapCount.IsEven(),
            indexSet
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, IndexSet> EGpIsNegativeId(this IndexSet id1, IndexSet id2)
    {
        var (swapCount, indexSet) = id1.SetMergeCountSwaps(id2);

        return new Tuple<bool, IndexSet>(
            swapCount.IsOdd(),
            indexSet
        );
    }
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSign(this IndexSet id1, IndexSet id2)
    {
        return EGpIsNegative(id1, id2) 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSign(this IndexSet id1, IndexSet id2, bool isNegative)
    {
        return EGpIsNegative(id1, id2) == isNegative
            ? IntegerSign.Positive
            : IntegerSign.Negative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpReverseSign(this IndexSet id1, IndexSet id2)
    {
        return EGpSign(id1, id2) * id2.Count.ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign OpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.SetOverlaps(id2)
            ? IntegerSign.Zero
            : EGpSign(id1, id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ESpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.Equals(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ELcpSign(this IndexSet id1, IndexSet id2)
    {
        return id2.SetIsSupersetOf(id1)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ERcpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.SetIsSupersetOf(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EFdpSign(this IndexSet id1, IndexSet id2)
    {
        return id2.SetIsSupersetOf(id1) || id1.SetIsSupersetOf(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EHipSign(this IndexSet id1, IndexSet id2)
    {
        return !id1.IsEmptySet && 
               !id2.IsEmptySet && 
               (id2.SetIsSupersetOf(id1) || id1.SetIsSupersetOf(id2))
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EAcpSign(this IndexSet id1, IndexSet id2)
    {
        //A acp B = (AB + BA) / 2
        return EGpIsNegative(id1, id2) == EGpIsNegative(id2, id1)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ECpSign(this IndexSet id1, IndexSet id2)
    {
        //A cp B = (AB - BA) / 2
        return EGpIsNegative(id1, id2) != EGpIsNegative(id2, id1)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of the given basis blades is -1.
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <param name="id3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsNegative(IndexSet id1, IndexSet id2, IndexSet id3)
    {
        return EGpIsNegative(id1, id2) != EGpIsNegative(id1 ^ id2, id3);
    }
}