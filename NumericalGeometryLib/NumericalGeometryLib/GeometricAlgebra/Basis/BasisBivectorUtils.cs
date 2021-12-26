﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    /// <summary>
    /// This class include some useful general utilities for computing Combinations
    /// of the form C(n, 2)
    /// </summary>
    public static class BasisBivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisBivectorIds(this ulong maxBasisBladeId)
        {
            var index = 0UL;
            var id = 3UL;

            while (id <= maxBasisBladeId)
            {
                yield return id;

                index++;
                id = index.BasisBivectorIndexToId();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisBivectorIndices(this ulong maxBasisBladeId)
        {
            var index = 0UL;
            var id = 3UL;

            while (id <= maxBasisBladeId)
            {
                yield return index;

                index++;
                id = index.BasisBivectorIndexToId();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetBasisBivectorVectorIndices(this ulong maxBasisBladeId)
        {
            var index = 0UL;
            var id = 3UL;

            while (id <= maxBasisBladeId)
            {
                yield return index.BasisBivectorIndexToVectorIndices();

                index++;
                id = index.BasisBivectorIndexToId();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBivectorIndexToMinVSpaceDimension(this ulong index)
        {
            return 1U + (uint) (0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisBivectorId(this ulong basisBladeId)
        {
            return basisBladeId.IsBasisBladeOfGrade(2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBivectorId(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index2 >= 0 && index1 != index2);

            return (1UL << index1) | (1UL << index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBivectorId(ulong index1, ulong index2)
        {
            Debug.Assert(index1 != index2);

            return (1UL << (int) index1) | (1UL << (int) index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord BasisBivectorIndexToVectorIndices(this ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return new IndexPairRecord(n1, n2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIndexToVectorIndex1(this ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return n1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIndexToVectorIndex2(this ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            //var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return n2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIndexToId(this ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return (1UL << (int) n1) | (1UL << (int) n2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBivectorIndex(this IndexPairRecord basisVectorIndexPair)
        {
            var (n1, n2) = basisVectorIndexPair;

            Debug.Assert(n1 != n2);

            return n1 < n2
                ? n1 + ((n2 * (n2 - 1UL)) >> 1)
                : n2 + ((n1 * (n1 - 1UL)) >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBivectorIndex(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index2 >= 0 && index1 != index2);

            var n1 = (ulong) index1;
            var n2 = (ulong) index2;

            return n1 < n2
                ? n1 + ((n2 * (n2 - 1UL)) >> 1)
                : n2 + ((n1 * (n1 - 1UL)) >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBivectorIndex(ulong index1, ulong index2)
        {
            Debug.Assert(index1 != index2);

            return index1 < index2
                ? index1 + ((index2 * (index2 - 1UL)) >> 1)
                : index2 + ((index1 * (index1 - 1UL)) >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIdToIndex(this ulong basisBladeId)
        {
            var n2 = (ulong) Math.Log(basisBladeId, 2);
            var n1 = (ulong) Math.Log(basisBladeId - (1UL << (int)n2), 2);
            
            return n1 + ((n2 * (n2 - 1UL)) >> 1);
        }


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorId(this ulong basisBivectorId)
        {
            return basisBivectorId <= BasisBladeDataLookup.MaxBasisBladeId && 
                   basisBivectorId.IsBasisBladeOfGrade(2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorId(this ulong basisBivectorId, uint vSpaceDimension)
        {
            return basisBivectorId < (1UL << (int) vSpaceDimension) && 
                   basisBivectorId.IsBasisBladeOfGrade(2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorIndex(this ulong basisBivectorIndex)
        {
            var kvDim = BasisBladeDataLookup.MaxVSpaceDimension.GetBinomialCoefficient(2);

            return basisBivectorIndex < kvDim;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorGradeIndex(uint grade, ulong basisBivectorIndex)
        {
            if (grade > BasisBladeDataLookup.MaxVSpaceDimension) 
                return false;

            var kvDim = BasisBladeDataLookup.MaxVSpaceDimension.GetBinomialCoefficient(2);

            return basisBivectorIndex < kvDim;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorIndex(ulong basisBivectorIndex, uint vSpaceDimension)
        {
            var kvDim = vSpaceDimension.GetBinomialCoefficient(2);

            return basisBivectorIndex < kvDim;
        }
    }
}
