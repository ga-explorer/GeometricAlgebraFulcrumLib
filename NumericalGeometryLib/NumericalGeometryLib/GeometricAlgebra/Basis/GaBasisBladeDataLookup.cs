using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    internal sealed class GaBasisBladeDataLookup
    {
        public static GaBasisBladeDataLookup Default { get; }
            = new GaBasisBladeDataLookup(12);


        private readonly GaBasisBladeData[] _basisBladeArray;
        private readonly GaBasisBladeData[][] _basisBladeGradedArray;


        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension { get; }


        private GaBasisBladeDataLookup(uint vSpaceDimension)
        {
            if (vSpaceDimension is < 2 or > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
            GaSpaceDimension = 1UL << (int) vSpaceDimension;

            _basisBladeArray = new GaBasisBladeData[GaSpaceDimension];
            _basisBladeGradedArray = new GaBasisBladeData[VSpaceDimension + 1][];

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = 
                    VSpaceDimension.GetBinomialCoefficient(grade);

                _basisBladeGradedArray[grade] = new GaBasisBladeData[kvSpaceDimension];
            }

            var indexCountArray = new int[VSpaceDimension + 1];

            for (var id = 0UL; id < GaSpaceDimension; id++)
            {
                var grade = (uint) BitOperations.PopCount((uint) id);
                var index = (ulong) indexCountArray[grade];
                indexCountArray[grade]++;

                var basisBlade = 
                    new GaBasisBladeData(GaSpaceDimension, id, grade, index);

                _basisBladeArray[id] = basisBlade;
                _basisBladeGradedArray[grade][index] = basisBlade;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint IdToGrade(ulong id)
        {
            return id < GaSpaceDimension 
                ? _basisBladeArray[id].Grade 
                : (uint) BitOperations.PopCount(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong IdToIndex(ulong id)
        {
            return id < (ulong) _basisBladeArray.Length 
                ? _basisBladeArray[id].Index 
                : id.CombinadicPatternToIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<uint, ulong> IdToGradeIndex(ulong id)
        {
            if (id < (ulong) _basisBladeArray.Length)
            {
                var basisBlade = _basisBladeArray[id];

                return new Tuple<uint, ulong>(
                    basisBlade.Grade,
                    basisBlade.Index
                );
            }

            id.CombinadicPatternToIndex(out var grade, out var index);

            return new Tuple<uint, ulong>((uint) grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GradeIndexToId(uint grade, ulong index)
        {
            if (grade >= _basisBladeGradedArray.Length) 
                return index.IndexToCombinadicPattern((int) grade);
            
            var table = 
                _basisBladeGradedArray[grade];

            return index < (ulong) table.Length 
                ? table[index].Id 
                : index.IndexToCombinadicPattern((int) grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp(ulong id)
        {
            return id >= GaSpaceDimension
                ? GaBasisBladeData.ComputeIsNegativeEGp(id, id)
                : _basisBladeArray[id].IsNegativeEGp(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp(ulong id1, ulong id2)
        {
            return id1 >= GaSpaceDimension || id2 >= GaSpaceDimension
                ? GaBasisBladeData.ComputeIsNegativeEGp(id1, id2)
                : _basisBladeArray[id1].IsNegativeEGp(id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp(uint grade, ulong index1, ulong index2)
        {
            var indexArray = _basisBladeGradedArray[grade];

            return indexArray[index1].IsNegativeEGp(indexArray[index2].Id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegativeEGp(uint grade1, ulong index1, uint grade2, ulong index2)
        {
            var indexArray1 = _basisBladeGradedArray[grade1];
            var indexArray2 = _basisBladeGradedArray[grade2];

            return indexArray1[index1].IsNegativeEGp(indexArray2[index2].Id);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(ulong id)
        {
            return IsNegativeEGp(id) ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(ulong id1, ulong id2)
        {
            return IsNegativeEGp(id1, id2) ? -1 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(uint grade, ulong index1, ulong index2)
        {
            return IsNegativeEGp(grade, index1, index2) ? -1 : 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(uint grade1, ulong index1, uint grade2, ulong index2)
        {
            return IsNegativeEGp(grade1, index1, grade2, index2) ? -1 : 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpReverseSignature(ulong id1, ulong id2)
        {
            var reverseSign = 
                HasNegativeReverse(id2) ? -1 : 1;

            return IsNegativeEGp(id1, id2) ? -reverseSign : reverseSign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ESpSignature(ulong id1, ulong id2)
        {
            if (GaMultivectorProductUtils.IsNonZeroESp(id1, id2))
                return IsNegativeEGp(id1, id2) ? -1 : 1;
            
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ENormSquaredSignature(ulong id)
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ENormSquaredSignature(ulong id1, ulong id2)
        {
            if (GaMultivectorProductUtils.IsZeroESp(id1, id2)) 
                return 0;

            var reverseSign = 
                HasNegativeReverse(id2) ? -1 : 1;

            return IsNegativeEGp(id1, id2) ? -reverseSign : reverseSign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int OpSignature(ulong id1, ulong id2)
        {
            if (GaMultivectorProductUtils.IsNonZeroOp(id1, id2))
                return IsNegativeEGp(id1, id2) ? -1 : 1;
            
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ELcpSignature(ulong id1, ulong id2)
        {
            if (GaMultivectorProductUtils.IsNonZeroELcp(id1, id2))
                return IsNegativeEGp(id1, id2) ? -1 : 1;
            
            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ERcpSignature(ulong id1, ulong id2)
        {
            if (GaMultivectorProductUtils.IsNonZeroERcp(id1, id2))
                return IsNegativeEGp(id1, id2) ? -1 : 1;
            
            return 0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNegativeGradeInvolution(ulong id)
        {
            return IdToGrade(id).GradeHasNegativeGradeInvolution();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNegativeReverse(ulong id)
        {
            return IdToGrade(id).GradeHasNegativeReverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNegativeCliffordConjugate(ulong id)
        {
            return IdToGrade(id).GradeHasNegativeCliffordConjugate();
        }
    }
}