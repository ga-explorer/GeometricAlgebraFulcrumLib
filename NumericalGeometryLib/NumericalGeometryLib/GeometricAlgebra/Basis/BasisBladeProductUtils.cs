using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    public static class BasisBladeProductUtils
    {
        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2, ulong id3)
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
        public static bool IsNonZeroEGp(ulong id1, ulong id2)
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
        public static bool IsNonZeroESp(ulong id1, ulong id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEHip(ulong id1, ulong id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEAcp(ulong id1, ulong id2)
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
        public static bool IsNonZeroECp(ulong id1, ulong id2)
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
            return IsNonZeroTriProductLeftAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcpELcpRa(ulong id1, ulong id2, ulong id3)
        {
            return IsNonZeroTriProductRightAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
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
        /// <param name="vSpaceDimension"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong JoinIDs(uint vSpaceDimension, ulong id1, ulong id2)
        {
            return id1 | (id2 << (int) vSpaceDimension);
        }


        ///// <summary>
        ///// Compute if the Euclidean Geometric Product of two basis blades is -1.
        ///// This method is slower than lookup, but can be used for GAs with dimension
        ///// more than 15
        ///// </summary>
        ///// <param name="id1"></param>
        ///// <returns></returns>
        //public static bool ComputeIsNegativeEGp(ulong id1)
        //{
        //    if (id1 == 0ul) 
        //        return false;

        //    var flag = false;
        //    var id = id1;

        //    //Find largest 1-bit of ID1 and create a bit mask
        //    var initMask1 = 1ul;
        //    while (initMask1 <= id1)
        //        initMask1 <<= 1;

        //    initMask1 >>= 1;

        //    var mask2 = 1ul;
        //    while (mask2 <= id1)
        //    {
        //        //If the current bit in ID2 is one:
        //        if ((id1 & mask2) != 0ul)
        //        {
        //            //Count number of swaps, each new swap inverts the final sign
        //            var mask1 = initMask1;

        //            while (mask1 > mask2)
        //            {
        //                if ((id & mask1) != 0ul)
        //                    flag = !flag;

        //                mask1 >>= 1;
        //            }
        //        }

        //        //Invert the corresponding bit in ID1
        //        id ^= mask2;

        //        mask2 <<= 1;
        //    }

        //    return flag;
        //}

        ///// <summary>
        ///// Compute if the Euclidean Geometric Product of two basis blades is -1.
        ///// This method is slower than lookup, but can be used for GAs with dimension
        ///// more than 15
        ///// </summary>
        ///// <param name="id1"></param>
        ///// <param name="id2"></param>
        ///// <returns></returns>
        //public static bool ComputeIsNegativeEGp(ulong id1, ulong id2)
        //{
        //    if (id1 == 0ul || id2 == 0ul) return false;

        //    var flag = false;
        //    var id = id1;

        //    //Find largest 1-bit of ID1 and create a bit mask
        //    var initMask1 = 1ul;
        //    while (initMask1 <= id1)
        //        initMask1 <<= 1;

        //    initMask1 >>= 1;

        //    var mask2 = 1ul;
        //    while (mask2 <= id2)
        //    {
        //        //If the current bit in ID2 is one:
        //        if ((id2 & mask2) != 0ul)
        //        {
        //            //Count number of swaps, each new swap inverts the final sign
        //            var mask1 = initMask1;

        //            while (mask1 > mask2)
        //            {
        //                if ((id & mask1) != 0ul)
        //                    flag = !flag;

        //                mask1 >>= 1;
        //            }
        //        }

        //        //Invert the corresponding bit in ID1
        //        id ^= mask2;

        //        mask2 <<= 1;
        //    }

        //    return flag;
        //}

        //public static int ComputeEGpSignature(ulong id1)
        //{
        //    if (id1 == 0ul) return 1;

        //    var signature = 1;
        //    var id = id1;

        //    //Find largest 1-bit of ID1 and create a bit mask
        //    var initMask1 = 
        //        id1.PatternToMask();

        //    //var initMask1 = 1ul;
        //    //while (initMask1 <= id1)
        //    //    initMask1 <<= 1;

        //    //initMask1 >>= 1;

        //    var mask2 = 1ul;
        //    while (mask2 <= id1)
        //    {
        //        //If the current bit in ID2 is one:
        //        if ((id1 & mask2) != 0ul)
        //        {
        //            //Count number of swaps, each new swap inverts the final sign
        //            var mask1 = initMask1;

        //            while (mask1 > mask2)
        //            {
        //                if ((id & mask1) != 0ul)
        //                    signature = -signature;

        //                mask1 >>= 1;
        //            }
        //        }

        //        //Invert the corresponding bit in ID1
        //        id ^= mask2;

        //        mask2 <<= 1;
        //    }

        //    return signature;
        //}

        //public static int ComputeEGpSignature(ulong id1, ulong id2)
        //{
        //    if (id1 == 0ul || id2 == 0ul) return 1;

        //    var signature = 1;
        //    var id = id1;

        //    //Find largest 1-bit of ID1 and create a bit mask
        //    var initMask1 = 1ul;
        //    while (initMask1 <= id1)
        //        initMask1 <<= 1;

        //    initMask1 >>= 1;

        //    var mask2 = 1ul;
        //    while (mask2 <= id2)
        //    {
        //        //If the current bit in ID2 is one:
        //        if ((id2 & mask2) != 0ul)
        //        {
        //            //Count number of swaps, each new swap inverts the final sign
        //            var mask1 = initMask1;

        //            while (mask1 > mask2)
        //            {
        //                if ((id & mask1) != 0ul)
        //                    signature = -signature;

        //                mask1 >>= 1;
        //            }
        //        }

        //        //Invert the corresponding bit in ID1
        //        id ^= mask2;

        //        mask2 <<= 1;
        //    }

        //    return signature;
        //}

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EGpSquaredIsPositive(ulong id)
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
        public static bool EGpSquaredIsNegative(ulong id)
        {
            return BasisBladeDataLookup.EGpIsNegative(id, id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpSquaredSign(ulong id)
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
        public static bool EGpIsNegative(ulong id1, ulong id2)
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
        public static bool EGpIsPositive(ulong id1, ulong id2)
        {
            return BasisBladeDataLookup.EGpIsNegative(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpSign(ulong id1, ulong id2)
        {
            return BasisBladeDataLookup.EGpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpReverseSign(ulong id1, ulong id2)
        {
            return BasisBladeDataLookup.EGpReverseSign(id1, id2);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static int EGpReverseSign(ulong id1, ulong id2)
        //{
        //    var signature = EGpSign(id1, id2);

        //    return id2.BasisBladeIdHasNegativeReverse()
        //        ? -signature
        //        : signature;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int OpSign(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0
                ? EGpSign(id1, id2)
                : 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ESpSign(ulong id1, ulong id2)
        {
            return id1 == id2
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ELcpSign(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ERcpSign(ulong id1, ulong id2)
        {
            return (~id1 & id2) == 0
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EFdpSign(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EHipSign(ulong id1, ulong id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EAcpSign(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return EGpIsNegative(id1, id2) == EGpIsNegative(id2, id1)
                ? EGpSign(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ECpSign(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return EGpIsNegative(id1, id2) != EGpIsNegative(id2, id1)
                ? EGpSign(id1, id2)
                : 0;
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
        public static bool EGpVectorBladeIsNegative(ulong index1, ulong id2)
        {
            return EGpIsNegative(index1.BasisVectorIndexToId(), id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EGpVectorBladeIsPositive(ulong index1, ulong id2)
        {
            return EGpIsPositive(index1.BasisVectorIndexToId(), id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpVectorBladeSign(ulong index1, ulong id2)
        {
            return EGpSign(index1.BasisVectorIndexToId(), id2);
        }
    }
}