using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    internal sealed class GaBasisBladeData
    {
        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slower than lookup, but can be used for GAs with dimension
        /// more than 15
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        internal static bool ComputeIsNegativeEGp(ulong id1, ulong id2)
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

        internal static int ComputeEGpSignature(ulong id1, ulong id2)
        {
            if (id1 == 0UL || id2 == 0UL) return 1;

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

            return signature;
        }

        
        private readonly BitArray _isNegativeEGpBitArray;


        public ulong Id { get; }

        public uint Grade { get; }

        public ulong Index { get; }


        internal GaBasisBladeData(ulong gaSpaceDimension, ulong id, uint grade, ulong index)
        {
            if (gaSpaceDimension > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(gaSpaceDimension));

            Id = id;
            Grade = grade;
            Index = index;
            _isNegativeEGpBitArray = new BitArray((int) gaSpaceDimension);

            for (var id2 = 0UL; id2 < gaSpaceDimension; id2++)
                _isNegativeEGpBitArray[(int) id2] = ComputeIsNegativeEGp(id, id2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp()
        {
            return _isNegativeEGpBitArray[(int) Id];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp(ulong id2)
        {
            return _isNegativeEGpBitArray[(int) id2];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature()
        {
            return _isNegativeEGpBitArray[(int) Id] ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(int id2)
        {
            return _isNegativeEGpBitArray[id2] ? -1 : 1;
        }
    }
}
