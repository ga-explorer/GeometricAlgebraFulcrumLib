using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis
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
        public static bool OpIsNonZero(this IIndexSet id1, IIndexSet id2)
        {
            return !id1.Overlaps(id2);
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
        public static bool EGpIsNonZero(this IIndexSet id1, IIndexSet id2)
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
        public static bool ESpIsNonZero(this IIndexSet id1, IIndexSet id2)
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
        public static bool ELcpIsNonZero(this IIndexSet id1, IIndexSet id2)
        {
            return id2.Contains(id1);
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
        public static bool ERcpIsNonZero(this IIndexSet id1, IIndexSet id2)
        {
            return id1.Contains(id2);
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
        public static bool EFdpIsNonZero(this IIndexSet id1, IIndexSet id2)
        {
            return id1.Contains(id2) || 
                   id2.Contains(id1);
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
        public static bool EHipIsNonZero(this IIndexSet id1, IIndexSet id2)
        {
            return !id1.IsEmptySet && 
                   !id2.IsEmptySet && 
                   (id1.Contains(id2) || id2.Contains(id1));
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
        public static bool EAcpIsNonZero(this IIndexSet id1, IIndexSet id2)
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
        public static bool ECpIsNonZero(this IIndexSet id1, IIndexSet id2)
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

        //public static IntegerSign ComputeEGpSignature(ulong id1)
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

        //public static IntegerSign ComputeEGpSignature(ulong id1, ulong id2)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EGpIsNegative(this UInt64IndexSet id1, UInt64IndexSet id2)
        {
            return BasisBladeDataLookup.EGpIsNegative(
                id1.IndexBitPattern, 
                id2.IndexBitPattern
            );
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool EGpIsNegative(this IIndexSet id1, IIndexSet id2)
        {
            if (id1.TryGetUInt64BitPattern(out var b1) && id2.TryGetUInt64BitPattern(out var b2))
                return BasisBladeDataLookup.EGpIsNegative(b1, b2);

            if (id1.IsEmptySet || id2.IsEmptySet)
                return false;
            
            var isNegative = false;
            
            // Active index range of index sets
            var id1IndexRange = new IndexRange(id1.Count - 1);
            var id2IndexRange = new IndexRange(id2.Count - 1);
            
            while (id1IndexRange.IsValid && id2IndexRange.IsValid)
            {
                var id1FirstIndex = id1[id1IndexRange.Index1];
                var id2FirstIndex = id2[id2IndexRange.Index1];

                while (id1FirstIndex == id2FirstIndex)
                {
                    id1IndexRange.IncreaseIndex1();
                    id2IndexRange.IncreaseIndex1();

                    if (id1IndexRange.IsCountOdd)
                        isNegative = !isNegative;

                    if (!id1IndexRange.IsValid || !id2IndexRange.IsValid)
                        return isNegative;

                    id1FirstIndex = id1[id1IndexRange.Index1];
                    id2FirstIndex = id2[id2IndexRange.Index1];
                }
                
                if (id1FirstIndex < id2FirstIndex)
                {
                    while (id1FirstIndex < id2FirstIndex)
                    {
                        id1IndexRange.IncreaseIndex1();

                        if (!id1IndexRange.IsValid)
                            return isNegative;

                        id1FirstIndex = id1[id1IndexRange.Index1];
                    }
                }
                else // id1FirstIndex > id2FirstIndex
                {
                    var swapFlag = id1IndexRange.IsCountOdd;

                    while (id1FirstIndex > id2FirstIndex)
                    {
                        if (swapFlag)
                            isNegative = !isNegative;
                        
                        id2IndexRange.IncreaseIndex1();

                        if (!id2IndexRange.IsValid)
                            return isNegative;

                        id2FirstIndex = id2[id2IndexRange.Index1];
                    }
                }
            }
            
            return isNegative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<bool, ulong> EGpIsNegativeId(this ulong id1, ulong id2)
        {
            return new Tuple<bool, ulong>(
                BasisBladeDataLookup.EGpIsNegative(id1, id2),
                id1 ^ id2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<bool, UInt64IndexSet> EGpIsNegativeId(this UInt64IndexSet id1, UInt64IndexSet id2)
        {
            var b1 = id1.IndexBitPattern;
            var b2 = id2.IndexBitPattern;

            return new Tuple<bool, UInt64IndexSet>(
                BasisBladeDataLookup.EGpIsNegative(b1, b2),
                (b1 ^ b2).BitPatternToUInt64IndexSet()
            );
        }

        public static Tuple<bool, IIndexSet> EGpIsNegativeId(this IIndexSet id1, IIndexSet id2)
        {
            if (id1.TryGetUInt64BitPattern(out var b1) && id2.TryGetUInt64BitPattern(out var b2))
            {
                return new Tuple<bool, IIndexSet>(
                    BasisBladeDataLookup.EGpIsNegative(b1, b2),
                    (b1 ^ b2).BitPatternToUInt64IndexSet()
                );
            }

            if (id1.IsEmptySet)
                return new Tuple<bool, IIndexSet>(false, id2);

            if (id2.IsEmptySet)
                return new Tuple<bool, IIndexSet>(false, id1);

            var isNegative = false;
            
            // Active index range of index sets
            var id1IndexRange = new IndexRange(id1.Count - 1);
            var id2IndexRange = new IndexRange(id2.Count - 1);

            var indexList = new List<int>(id1.Count + id2.Count);
                
            while (id1IndexRange.IsValid && id2IndexRange.IsValid)
            {
                var id1FirstIndex = id1[id1IndexRange.Index1];
                var id2FirstIndex = id2[id2IndexRange.Index1];

                while (id1FirstIndex == id2FirstIndex)
                {
                    id1IndexRange.IncreaseIndex1();
                    id2IndexRange.IncreaseIndex1();

                    if (id1IndexRange.IsCountOdd)
                        isNegative = !isNegative;

                    if (!id1IndexRange.IsValid || !id2IndexRange.IsValid)
                        break;

                    id1FirstIndex = id1[id1IndexRange.Index1];
                    id2FirstIndex = id2[id2IndexRange.Index1];
                }

                // One or both of the two sets is empty, no more swaps are needed
                if (!id1IndexRange.IsValid || !id2IndexRange.IsValid)
                    break;

                if (id1FirstIndex < id2FirstIndex)
                {
                    while (id1FirstIndex < id2FirstIndex)
                    {
                        indexList.Add(id1FirstIndex);

                        id1IndexRange.IncreaseIndex1();

                        if (!id1IndexRange.IsValid)
                            break;

                        id1FirstIndex = id1[id1IndexRange.Index1];
                    }

                    if (!id1IndexRange.IsValid)
                        break;
                }
                else // id1FirstIndex > id2FirstIndex
                {
                    var swapFlag = id1IndexRange.IsCountOdd;

                    while (id1FirstIndex > id2FirstIndex)
                    {
                        indexList.Add(id2FirstIndex);

                        if (swapFlag)
                            isNegative = !isNegative;
                        
                        id2IndexRange.IncreaseIndex1();

                        if (!id2IndexRange.IsValid)
                            break;

                        id2FirstIndex = id2[id2IndexRange.Index1];
                    }

                    if (!id2IndexRange.IsValid)
                        break;
                }
            }
            
            if (!id1IndexRange.IsValid)
            {
                if (id2IndexRange.IsValid)
                    indexList.AddRange(
                        id2IndexRange.GetItems(id2)
                    );
            }
            else if (!id2IndexRange.IsValid)
            {
                if (id1IndexRange.IsValid)
                    indexList.AddRange(
                        id1IndexRange.GetItems(id1)
                    );
            }
            
            return new Tuple<bool, IIndexSet>(
                isNegative,
                indexList.ToIndexSet()
            );

            //// This is 5 times slower!
            //var isNegative = false;
            //var indexList = new List<int>(id1.Count + id2.Count);

            //if (id1.Count > 0)
            //    indexList.AddRange(id1);

            //foreach (var basisVector2Index in id2)
            //{
            //    if (indexList.Count == 0)
            //    {
            //        indexList.Add(basisVector2Index);
            //        continue;
            //    }

            //    var j = indexList.Count - 1;
            //    while (j >= 0)
            //    {
            //        var basisVector1Index = indexList[j];

            //        if (basisVector1Index > basisVector2Index)
            //        {
            //            isNegative = !isNegative;
            //            j--;

            //            if (j >= 0) continue;

            //            indexList.Insert(0, basisVector2Index);
            //        }
            //        else
            //        {
            //            if (basisVector1Index == basisVector2Index)
            //                indexList.RemoveAt(j);
            //            else
            //                indexList.Insert(j + 1, basisVector2Index);
            //        }

            //        break;
            //    }
            //}

            //return new Tuple<bool, IIndexSet>(
            //    isNegative,
            //    indexList.ToImmutableSortedSet().ToIndexSet()
            //);
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
        public static bool EGpIsPositive(this IIndexSet id1, IIndexSet id2)
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
        public static IntegerSign EGpSign(this IIndexSet id1, IIndexSet id2)
        {
            return EGpIsNegative(id1, id2) 
                ? IntegerSign.Negative 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign EGpSign(this IIndexSet id1, IIndexSet id2, bool isNegative)
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
        public static IntegerSign EGpReverseSign(this IIndexSet id1, IIndexSet id2)
        {
            return EGpSign(id1, id2) * id2.Count.ReverseSignOfGrade();
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IntegerSign EGpReverseSign(ulong id1, ulong id2)
        //{
        //    var signature = EGpSign(id1, id2);

        //    return id2.BasisBladeIdHasNegativeReverse()
        //        ? -signature
        //        : signature;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign OpSign(this ulong id1, ulong id2)
        {
            return (id1 & id2) == 0
                ? EGpSign(id1, id2)
                : IntegerSign.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign OpSign(this IIndexSet id1, IIndexSet id2)
        {
            return id1.Overlaps(id2)
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
        public static IntegerSign ESpSign(this IIndexSet id1, IIndexSet id2)
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
        public static IntegerSign ELcpSign(this IIndexSet id1, IIndexSet id2)
        {
            return id2.Contains(id1)
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
        public static IntegerSign ERcpSign(this IIndexSet id1, IIndexSet id2)
        {
            return id1.Contains(id2)
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
        public static IntegerSign EFdpSign(this IIndexSet id1, IIndexSet id2)
        {
            return id2.Contains(id1) || id1.Contains(id2)
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
        public static IntegerSign EHipSign(this IIndexSet id1, IIndexSet id2)
        {
            return !id1.IsEmptySet && 
                   !id2.IsEmptySet && 
                   (id2.Contains(id1) || id1.Contains(id2))
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
        public static IntegerSign EAcpSign(this IIndexSet id1, IIndexSet id2)
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
        public static IntegerSign ECpSign(this IIndexSet id1, IIndexSet id2)
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
}