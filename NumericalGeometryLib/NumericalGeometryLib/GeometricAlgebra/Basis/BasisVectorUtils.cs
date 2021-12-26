﻿using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    public static class BasisVectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisVectorIds(this ulong maxBasisBladeId)
        {
            var index = 0UL;
            var id = 1UL;

            while (id <= maxBasisBladeId)
            {
                yield return id;

                index++;
                id = index.BasisVectorIndexToId();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisVectorIndices(this ulong maxBasisBladeId)
        {
            var index = 0UL;
            var id = 1UL;

            while (id <= maxBasisBladeId)
            {
                yield return index;

                index++;
                id = index.BasisVectorIndexToId();
            }
        }

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
            return (ulong) basisVectorId.FirstOneBitPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisVectorId(this ulong basisBladeId)
        {
            return BitOperations.PopCount(basisBladeId) == 1;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this ulong basisVectorId)
        {
            return basisVectorId <= BasisBladeDataLookup.MaxBasisBladeId && 
                   BitOperations.PopCount(basisVectorId) == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this ulong basisVectorId, uint vSpaceDimension)
        {
            return basisVectorId < (1UL << (int) vSpaceDimension) && 
                   BitOperations.PopCount(basisVectorId) == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this int index)
        {
            return index >= 0 && index < BasisBladeDataLookup.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this ulong basisVectorIndex)
        {
            return basisVectorIndex < BasisBladeDataLookup.MaxVSpaceDimension;
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