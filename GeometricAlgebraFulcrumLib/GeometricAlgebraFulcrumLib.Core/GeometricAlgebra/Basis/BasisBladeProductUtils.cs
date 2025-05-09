using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;

public static class BasisBladeProductUtils
{
    /// <summary>
    /// True if the outer product of the given euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool OpIsNonZero(this ulong id1, ulong id2)
    {
        return (id1 & id2) == 0;
    }

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
    public static bool OpIsNonZero(ulong id1, ulong id2, ulong id3)
    {
        return (id1 & id2 & id3) == 0;
    }

    /// <summary>
    /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsNonZero(this ulong id1, ulong id2)
    {
        return true;
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
    public static bool ESpIsNonZero(this ulong id1, ulong id2)
    {
        return id1 == id2;
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
    public static bool ELcpIsNonZero(this ulong id1, ulong id2)
    {
        return (id1 & ~id2) == 0;
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
        return id2.SetContains(id1);
    }

    /// <summary>
    /// True if the right contraction product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ERcpIsNonZero(this ulong id1, ulong id2)
    {
        return (id2 & ~id1) == 0;
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
        return id1.SetContains(id2);
    }

    /// <summary>
    /// True if the fat-dot product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EFdpIsNonZero(this ulong id1, ulong id2)
    {
        return (id1 & ~id2) == 0 || 
               (id2 & ~id1) == 0;
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
        return id1.SetContains(id2) || 
               id2.SetContains(id1);
    }

    /// <summary>
    /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EHipIsNonZero(this ulong id1, ulong id2)
    {
        return id1 != 0 && 
               id2 != 0 && 
               ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
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
               (id1.SetContains(id2) || id2.SetContains(id1));
    }

    /// <summary>
    /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EAcpIsNonZero(this ulong id1, ulong id2)
    {
        //A acp B = (AB + BA) / 2
        return EGpIsNegative(id1, id2) == EGpIsNegative(id2, id1);
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
    public static bool ECpIsNonZero(this ulong id1, ulong id2)
    {
        //A cp B = (AB - BA) / 2
        return EGpIsNegative(id1, id2) != EGpIsNegative(id2, id1);
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
    public static bool IsNonZeroTriProductLeftAssociative(ulong id1, ulong id2, ulong id3, Func<ulong, ulong, bool> isNonZeroProductFunc1, Func<ulong, ulong, bool> isNonZeroProductFunc2)
    {
        return isNonZeroProductFunc1(id1, id2) && isNonZeroProductFunc2(id1 ^ id2, id3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroTriProductRightAssociative(ulong id1, ulong id2, ulong id3, Func<ulong, ulong, bool> isNonZeroProductFunc1, Func<ulong, ulong, bool> isNonZeroProductFunc2)
    {
        return isNonZeroProductFunc1(id1, id2 ^ id3) && isNonZeroProductFunc2(id2, id3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroELcpELcpLa(ulong id1, ulong id2, ulong id3)
    {
        return IsNonZeroTriProductLeftAssociative(id1, id2, id3, ELcpIsNonZero, ELcpIsNonZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonZeroELcpELcpRa(ulong id1, ulong id2, ulong id3)
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
    public static ulong JoinIDs(ulong id1, ulong id2)
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
    public static ulong JoinIDs(uint vSpaceDimensions, ulong id1, ulong id2)
    {
        return id1 | (id2 << (int) vSpaceDimensions);
    }


    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpSquaredIsPositive(this ulong id)
    {
        return BasisBladeDataLookup.EGpIsPositive(id, id);
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpSquaredIsNegative(this ulong id)
    {
        return BasisBladeDataLookup.EGpIsNegative(id, id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSquaredSign(this ulong id)
    {
        return BasisBladeDataLookup.EGpSquaredSign(id);
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsNegative(this ulong id1, ulong id2)
    {
        return BasisBladeDataLookup.EGpIsNegative(id1, id2);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<bool, ulong> EGpIsNegativeId(this ulong id1, ulong id2)
    {
        return new Tuple<bool, ulong>(
            //BasisBladeDataComputer.EGpIsNegative(id1, id2),
            BasisBladeDataLookup.EGpIsNegative(id1, id2),
            id1 ^ id2
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

    /// <summary>
    /// Find if the Euclidean Geometric Product of two basis blades is -1.
    /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpIsPositive(this ulong id1, ulong id2)
    {
        return !BasisBladeDataLookup.EGpIsNegative(id1, id2);
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
        return !EGpIsNegative(id1, id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSign(this ulong id1, ulong id2)
    {
        return BasisBladeDataLookup.EGpSign(id1, id2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpSign(this ulong id1, ulong id2, bool isNegative)
    {
        return BasisBladeDataLookup.EGpIsNegative(id1, id2) == isNegative
            ? IntegerSign.Positive 
            : IntegerSign.Negative;
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
    public static IntegerSign EGpReverseSign(this ulong id1, ulong id2)
    {
        return BasisBladeDataLookup.EGpReverseSign(id1, id2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpReverseSign(this IndexSet id1, IndexSet id2)
    {
        return EGpSign(id1, id2) * id2.Count.ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign OpSign(this ulong id1, ulong id2)
    {
        return (id1 & id2) == 0
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign OpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.SetOverlaps(id2)
            ? IntegerSign.Zero
            : EGpSign(id1, id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ESpSign(this ulong id1, ulong id2)
    {
        return id1 == id2
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ESpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.Equals(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ELcpSign(this ulong id1, ulong id2)
    {
        return (id1 & ~id2) == 0
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ELcpSign(this IndexSet id1, IndexSet id2)
    {
        return id2.SetContains(id1)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ERcpSign(this ulong id1, ulong id2)
    {
        return (~id1 & id2) == 0
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ERcpSign(this IndexSet id1, IndexSet id2)
    {
        return id1.SetContains(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EFdpSign(this ulong id1, ulong id2)
    {
        return (id1 & ~id2) == 0 || (id2 & ~id1) == 0
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EFdpSign(this IndexSet id1, IndexSet id2)
    {
        return id2.SetContains(id1) || id1.SetContains(id2)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EHipSign(this ulong id1, ulong id2)
    {
        return id1 != 0 && 
               id2 != 0 && 
               ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EHipSign(this IndexSet id1, IndexSet id2)
    {
        return !id1.IsEmptySet && 
               !id2.IsEmptySet && 
               (id2.SetContains(id1) || id1.SetContains(id2))
            ? EGpSign(id1, id2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EAcpSign(this ulong id1, ulong id2)
    {
        //A acp B = (AB + BA) / 2
        return EGpIsNegative(id1, id2) == EGpIsNegative(id2, id1)
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
    public static IntegerSign ECpSign(this ulong id1, ulong id2)
    {
        //A cp B = (AB - BA) / 2
        return EGpIsNegative(id1, id2) != EGpIsNegative(id2, id1)
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
    public static bool EGpIsNegative(ulong id1, ulong id2, ulong id3)
    {
        return EGpIsNegative(id1, id2) != EGpIsNegative(id1 ^ id2, id3);
    }

    /// <summary>
    /// Find if the Euclidean Geometric Product of a basis vector and a basis blade is -1.
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpVectorBladeIsNegative(this ulong index1, ulong id2)
    {
        return EGpIsNegative(index1.BasisVectorIndexToId(), id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EGpVectorBladeIsPositive(this ulong index1, ulong id2)
    {
        return EGpIsPositive(index1.BasisVectorIndexToId(), id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign EGpVectorBladeSign(this ulong index1, ulong id2)
    {
        return EGpSign(index1.BasisVectorIndexToId(), id2);
    }
}