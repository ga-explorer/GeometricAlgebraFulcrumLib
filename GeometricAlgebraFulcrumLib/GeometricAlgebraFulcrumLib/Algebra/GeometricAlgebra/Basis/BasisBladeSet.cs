using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    /// <summary>
    /// This class holds information about a set of GA basis blades
    /// with orthonormal signature
    /// </summary>
    public sealed class BasisBladeSet
    {
        private static readonly Dictionary<Triplet<ulong>, BasisBladeSet> BasisSetCache
            = new Dictionary<Triplet<ulong>, BasisBladeSet>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearBasisSetCache()
        {
            BasisSetCache.Clear();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Triplet<ulong> CreateBasisSetSignature(IEnumerable<char> basisVectorSignatures)
        {
            return CreateBasisSetSignature(
                basisVectorSignatures.Select(c => 
                    c switch
                    {
                        '+' or 'p' or 'P' => 1,
                        '-' or 'n' or 'N' => -1,
                        _ => 0
                    }
                ).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Triplet<ulong> CreateBasisSetSignature(IEnumerable<int> basisVectorSignatures)
        {
            return CreateBasisSetSignature(basisVectorSignatures.ToArray());
        }

        private static Triplet<ulong> CreateBasisSetSignature(params int[] basisVectorSignatures)
        {
            if (basisVectorSignatures.Length is < 2 or > 63)
                throw new ArgumentOutOfRangeException();

            var positiveMask = 0UL;
            var negativeMask = 0UL;
            var zeroMask = 0UL;

            for (var i = 0; i < basisVectorSignatures.Length; i++)
            {
                var signature = basisVectorSignatures[i];

                if (signature > 0)
                    positiveMask |= 1UL << i;

                else if (signature < 0)
                    negativeMask |= 1UL << i;

                else
                    zeroMask |= 1UL << i;
            }

            return new Triplet<ulong>(
                positiveMask,
                negativeMask,
                zeroMask
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Triplet<ulong> CreateBasisSetSignature(uint positiveCount)
        {
            if (positiveCount is < 2 or > 63)
                throw new ArgumentOutOfRangeException();

            return new Triplet<ulong>(
                (1UL << (int) positiveCount) - 1,
                0UL,
                0UL
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Triplet<ulong> CreateBasisSetSignature(uint positiveCount, uint negativeCount)
        {
            var vSpaceDimension = positiveCount + negativeCount;

            if (vSpaceDimension is < 2 or > 63)
                throw new ArgumentOutOfRangeException();

            return new Triplet<ulong>(
                (1UL << (int) positiveCount) - 1,
                ((1UL << (int) negativeCount) - 1) << (int) positiveCount,
                0UL
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Triplet<ulong> CreateBasisSetSignature(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var vSpaceDimension = positiveCount + negativeCount + zeroCount;

            if (vSpaceDimension is < 2 or > 63)
                throw new ArgumentOutOfRangeException();

            return new Triplet<ulong>(
                (1UL << (int) positiveCount) - 1,
                ((1UL << (int) negativeCount) - 1) << (int) positiveCount,
                ((1UL << (int) zeroCount) - 1) << (int) (positiveCount + negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BasisBladeSet Create(Triplet<ulong> basisSetSignature)
        {
            if (BasisSetCache.TryGetValue(basisSetSignature, out var basisSet))
                return basisSet;

            basisSet = new BasisBladeSet(basisSetSignature);

            BasisSetCache.Add(basisSetSignature, basisSet);

            return basisSet;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet Create(IEnumerable<char> basisVectorSignatures)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(basisVectorSignatures);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet Create(IEnumerable<int> basisVectorSignatures)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(basisVectorSignatures);

            return Create(basisSetSignature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet Create(params int[] basisVectorSignatures)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(basisVectorSignatures);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet Create(uint positiveCount, uint negativeCount)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(positiveCount, negativeCount);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet Create(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(positiveCount, negativeCount, zeroCount);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet CreateEuclidean(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet CreateProjective(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension - 1, 0, 1);

            return Create(basisSetSignature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet CreateConformal(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension - 1, 1);

            return Create(basisSetSignature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBladeSet CreateMotherAlgebra(uint vSpaceDimension)
        {
            if (!vSpaceDimension.IsEven())
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));
            
            var n = vSpaceDimension / 2;
            var basisSetSignature = 
                CreateBasisSetSignature(n, n);

            return Create(basisSetSignature);
        }


        private readonly ulong _positiveMask;
        private readonly ulong _negativeMask;
        private readonly ulong _zeroMask;


        public double ZeroEpsilon { get; set; } 
            = 1e-12d;

        public uint VSpaceDimension 
            => PositiveCount + NegativeCount + ZeroCount;

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => GaSpaceDimension - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public Triplet<ulong> BasisSetSignature { get; }

        /// <summary>
        /// The number of basis vectors with signature equal to +1
        /// </summary>
        public uint PositiveCount { get; }

        /// <summary>
        /// The number of basis vectors with signature equal to -1
        /// </summary>
        public uint NegativeCount { get; }

        /// <summary>
        /// The number of basis vectors with signature equal to 0
        /// </summary>
        public uint ZeroCount { get; }

        public bool IsEuclidean 
            => NegativeCount + ZeroCount == 0;

        public bool IsProjective 
            => NegativeCount == 0 && ZeroCount == 1;

        public bool IsConformal 
            => NegativeCount == 1 && ZeroCount == 0;

        public bool IsMotherAlgebra 
            => PositiveCount == NegativeCount && ZeroCount == 0;

        public IReadOnlyList<int> BasisVectorSignatureList { get; }

        //public GaTerm PseudoScalar { get; }

        //public GaTerm PseudoScalarReverse { get; }

        //public GaTerm PseudoScalarEInverse { get; }

        //public GaTerm PseudoScalarInverse { get; }


        private BasisBladeSet(Triplet<ulong> basisSetSignature)
        {
            (_positiveMask, _negativeMask, _zeroMask) = basisSetSignature;
            BasisSetSignature = basisSetSignature;
            PositiveCount = (uint) BitOperations.PopCount(_positiveMask);
            NegativeCount = (uint) BitOperations.PopCount(_negativeMask);
            ZeroCount = (uint) BitOperations.PopCount(_zeroMask);

            var vSpaceDimension = 
                PositiveCount + NegativeCount + ZeroCount;

            Debug.Assert(
                vSpaceDimension is > 1 and < 64 && 
                BitOperations.PopCount(1UL + (_positiveMask | _negativeMask | _zeroMask)) == 1 &&
                (_positiveMask & _negativeMask) == 0 &&
                (_negativeMask & _zeroMask) == 0 &&
                (_zeroMask & _positiveMask) == 0
            );

            var basisVectorSignatureList = new int[vSpaceDimension];

            foreach (var i in _positiveMask.PatternToPositions())
                basisVectorSignatureList[i] = 1;
            
            foreach (var i in _negativeMask.PatternToPositions())
                basisVectorSignatureList[i] = -1;

            BasisVectorSignatureList = basisVectorSignatureList;

            //PseudoScalar = new GaTerm(this, MaxBasisBladeId, 1);
            //PseudoScalarReverse = PseudoScalar.Reverse();
            //PseudoScalarInverse = PseudoScalar.Inverse();
            //PseudoScalarEInverse = PseudoScalar.EInverse();
        }

        private BasisBladeSet(IReadOnlyList<int> basisVectorSignatureList)
        {
            Debug.Assert(
                basisVectorSignatureList.Count is > 1 and < 64 && 
                basisVectorSignatureList.All(s => s is -1 or 0 or 1)
            );

            BasisVectorSignatureList = basisVectorSignatureList;

            var i = 0;
            foreach (var basisVectorSignature in basisVectorSignatureList)
            {
                if (basisVectorSignature == 1)
                {
                    _positiveMask |= 1UL << i;
                    PositiveCount++;
                }
                else if (basisVectorSignature == -1)
                {
                    _negativeMask |= 1UL << i;
                    NegativeCount++;
                }
                else
                {
                    _zeroMask |= 1UL << i;
                    ZeroCount++;
                }

                i++;
            }

            BasisSetSignature = new Triplet<ulong>(
                _positiveMask, 
                _negativeMask, 
                _zeroMask
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong KvSpaceDimension(uint grade)
        {
            return VSpaceDimension.GetBinomialCoefficient(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            var id = 1UL << index;

            if ((id & _positiveMask) != 0UL)
                return 1;

            if ((id & _negativeMask) != 0UL)
                return -1;

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            var id = 1UL << (int) index;

            if ((id & _positiveMask) != 0UL)
                return 1;

            if ((id & _negativeMask) != 0UL)
                return -1;

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(
                index1 >= 0 &&
                index2 >= index1 && 
                index2 < VSpaceDimension
            );

            return GetBasisVectorSignature(index1) *
                   GetBasisVectorSignature(index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(
                index2 >= index1 && 
                index2 < VSpaceDimension
            );

            return GetBasisVectorSignature(index1) *
                   GetBasisVectorSignature(index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSquaredSign(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ConjugateIsNegative(ulong id)
        {
            return ConjugateSign(id) == -1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ConjugateIsPositive(ulong id)
        {
            return ConjugateSign(id) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ConjugateSign(ulong id)
        {
            var reverseSign = 
                BitOperations.PopCount(id).ReverseSignOfGrade();

            return (BitOperations.PopCount(id & _negativeMask) & 1) == 0
                ? reverseSign 
                : -reverseSign;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GradeInvolutionSign(ulong id)
        {
            return BitOperations.PopCount(id).GradeInvolutionSignOfGrade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReverseSign(ulong id)
        {
            return BitOperations.PopCount(id).ReverseSignOfGrade();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CliffordConjugateSign(ulong id)
        {
            return BitOperations.PopCount(id).CliffordConjugateSignOfGrade();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSquaredSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSquaredSign(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpReverseSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSquaredSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSquaredSign(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ESpSquaredSign(ulong id)
        {
            return EGpSquaredSign(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ENormSquaredSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSquaredSign(ulong id)
        {
            return GpSquaredSign(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NormSquaredSign(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSign(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpReverseSign(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpReverseSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSign(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(commonBasisBladesId & _negativeMask);

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSign(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSign(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;
            
            var euclideanSignature = 
                BasisBladeProductUtils.EGpReverseSign(id1, id2);

            var negativeBasisCount = 
                BitOperations.PopCount(commonBasisBladesId & _negativeMask);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature 
                : -euclideanSignature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int OpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroOp(id1, id2)
                ? 0 
                : EGpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroESp(id1, id2)
                ? 0 
                : GpSquaredSign(id1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ELcpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroELcp(id1, id2)
                ? 0 
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LcpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroELcp(id1, id2)
                ? 0 
                : GpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ERcpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroERcp(id1, id2)
                ? 0 
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RcpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroERcp(id1, id2)
                ? 0 
                : GpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EFdpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEFdp(id1, id2)
                ? 0
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FdpSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEFdp(id1, id2)
                ? 0
                : GpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EHipSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEHip(id1, id2)
                ? 0 
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HipSign(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEHip(id1, id2)
                ? 0 
                : GpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EAcpSign(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return GaMultivectorProductUtils.IsZeroEAcp(id1, id2)
                ? 0
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AcpSign(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return GaMultivectorProductUtils.IsZeroEAcp(id1, id2)
                ? 0
                : GpSign(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ECpSign(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return GaMultivectorProductUtils.IsZeroECp(id1, id2)
                ? 0
                : EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CpSign(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return GaMultivectorProductUtils.IsZeroECp(id1, id2)
                ? 0
                : GpSign(id1, id2);
        }
    }
}
