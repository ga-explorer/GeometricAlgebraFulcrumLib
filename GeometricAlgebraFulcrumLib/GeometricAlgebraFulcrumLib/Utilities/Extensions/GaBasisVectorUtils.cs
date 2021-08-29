using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaBasisVectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisVectorIndexToMinVSpaceDimension(this ulong index)
        {
            return 1U + (uint) index;
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndexToId(this int index)
        {
            return 1UL << index;
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndexToId(this uint index)
        {
            return 1UL << (int) index;
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndexToId(this ulong index)
        {
            return 1UL << (int) index;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIdToIndex(this ulong basisVectorId)
        {
            return basisVectorId < (ulong)GaLookupTables.IdToIndexTable.Length
                ? GaLookupTables.IdToIndexTable[basisVectorId]
                : (ulong) basisVectorId.FirstOneBitPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisVectorId(this ulong basisBladeId)
        {
            return basisBladeId.IsBasicPattern();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this ulong basisVectorId)
        {
            return basisVectorId <= GaSpaceUtils.MaxVSpaceBasisBladeId && 
                   basisVectorId.IsBasicPattern();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this ulong basisVectorId, uint vSpaceDimension)
        {
            return basisVectorId < vSpaceDimension.ToGaSpaceDimension() && 
                   basisVectorId.IsBasicPattern();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this int index)
        {
            return index >= 0 && index < GaSpaceUtils.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this ulong basisVectorIndex)
        {
            return basisVectorIndex < GaSpaceUtils.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this int basisVectorIndex, uint vSpaceDimension)
        {
            return basisVectorIndex >= 0 && basisVectorIndex < vSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this ulong basisVectorIndex, uint vSpaceDimension)
        {
            return basisVectorIndex < vSpaceDimension;
        }


        public static bool TryGetValidBasisVectorIndex(this ulong basisBladeId, out ulong basisVectorIndex)
        {
            var onesCount = 0;
            basisVectorIndex = 0;

            var i = 0UL;
            while (basisBladeId != 0 && onesCount <= 1)
            {
                if ((basisBladeId & 1) != 0)
                {
                    basisVectorIndex = (onesCount == 0) ? i : 0UL;
                    onesCount++;
                }

                i++;
                basisBladeId >>= 1;
            }

            return onesCount == 1;
        }
    }
}