using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis
{
    internal static class BasisBladeDataComputer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeGrade(ulong id)
        {
            return (uint) BitOperations.PopCount(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIndex(ulong id)
        {
            return id.CombinadicPatternToIndex();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<uint, ulong> BasisBladeGradeIndex(ulong id)
        {
            return new Tuple<uint, ulong>(
                (uint) BitOperations.PopCount(id), 
                id.CombinadicPatternToIndex()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeId(uint grade, ulong index)
        {
            return index.IndexToCombinadicPattern((int) grade);
        }

        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slower than lookup, but can be used for GAs with dimension
        /// more than 15
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool EGpIsNegative(ulong id1, ulong id2)
        {
            if (id1 == 0UL || id2 == 0UL) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1UL;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1UL;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EGpIsPositive(ulong id1, ulong id2)
        {
            return !EGpIsNegative(id1, id2);
        }

        public static IntegerSign EGpSign(ulong id1, ulong id2)
        {
            if (id1 == 0UL || id2 == 0UL) return IntegerSign.Positive;

            var signature = 1;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1UL;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1UL;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            signature = -signature;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return signature > 0 
                ? IntegerSign.Positive 
                : IntegerSign.Negative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign EGpReverseSign(ulong id1, ulong id2)
        {
            var egpSign = EGpSign(id1, id2);

            return ((uint) BitOperations.PopCount(id2)).ReverseIsNegativeOfGrade()
                ? -egpSign 
                : egpSign;
        }
    }
}