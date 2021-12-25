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

namespace NumericalGeometryLib.GeometricAlgebra.Basis
{
    /// <summary>
    /// This class holds information about a set of GA basis blades
    /// with orthonormal signature 1,-1,0
    /// </summary>
    public sealed class GaBasisSet
    {
        private static GaBasisBladeDataLookup Lookup 
            => GaBasisBladeDataLookup.Default;


        private static readonly Dictionary<Triplet<ulong>, GaBasisSet> BasisSetCache
            = new Dictionary<Triplet<ulong>, GaBasisSet>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearBasisSetCache()
        {
            BasisSetCache.Clear();
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
        private static GaBasisSet Create(Triplet<ulong> basisSetSignature)
        {
            if (BasisSetCache.TryGetValue(basisSetSignature, out var basisSet))
                return basisSet;

            basisSet = new GaBasisSet(basisSetSignature);

            BasisSetCache.Add(basisSetSignature, basisSet);

            return basisSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet Create(uint positiveCount, uint negativeCount)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(positiveCount, negativeCount);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet Create(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(positiveCount, negativeCount, zeroCount);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateEuclidean(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension);

            return Create(basisSetSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateProjective(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension - 1, 0, 1);

            return Create(basisSetSignature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateConformal(uint vSpaceDimension)
        {
            var basisSetSignature = 
                CreateBasisSetSignature(vSpaceDimension - 1, 1);

            return Create(basisSetSignature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisSet CreateMotherAlgebra(uint vSpaceDimension)
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

        public GaTerm PseudoScalar { get; }

        public GaTerm PseudoScalarReverse { get; }

        public GaTerm PseudoScalarEInverse { get; }

        public GaTerm PseudoScalarInverse { get; }


        private GaBasisSet(Triplet<ulong> basisSetSignature)
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

            PseudoScalar = new GaTerm(this, MaxBasisBladeId, 1);
            PseudoScalarReverse = PseudoScalar.Reverse();
            PseudoScalarInverse = PseudoScalar.Inverse();
            PseudoScalarEInverse = PseudoScalar.EInverse();
        }

        private GaBasisSet(IReadOnlyList<int> basisVectorSignatureList)
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
            var id = Lookup.GradeIndexToId(grade, index);

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
                Lookup.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return Lookup.EGpSignature(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            return Lookup.EGpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EGpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            return Lookup.EGpReverseSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            var euclideanSignature = 
                Lookup.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(commonBasisBladesId & _negativeMask);

            var euclideanSignature = 
                Lookup.EGpSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension && id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(commonBasisBladesId & _negativeMask);

            var euclideanSignature = 
                Lookup.EGpReverseSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int OpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroOp(id1, id2)
                ? 0 
                : EGpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ESpSquaredSignature(ulong id)
        {
            return EGpSquaredSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSquaredSignature(ulong id)
        {
            return GpSquaredSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroESp(id1, id2)
                ? 0 
                : GpSquaredSignature(id1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ENormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                BitOperations.PopCount(id & _negativeMask);

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ELcpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroELcp(id1, id2)
                ? 0 
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LcpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroELcp(id1, id2)
                ? 0 
                : GpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ERcpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroERcp(id1, id2)
                ? 0 
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RcpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroERcp(id1, id2)
                ? 0 
                : GpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EFdpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEFdp(id1, id2)
                ? 0
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FdpSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEFdp(id1, id2)
                ? 0
                : GpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EHipSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEHip(id1, id2)
                ? 0 
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HipSignature(ulong id1, ulong id2)
        {
            return GaMultivectorProductUtils.IsZeroEHip(id1, id2)
                ? 0 
                : GpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EAcpSignature(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return GaMultivectorProductUtils.IsZeroEAcp(id1, id2)
                ? 0
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AcpSignature(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return GaMultivectorProductUtils.IsZeroEAcp(id1, id2)
                ? 0
                : GpSignature(id1, id2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ECpSignature(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return GaMultivectorProductUtils.IsZeroECp(id1, id2)
                ? 0
                : EGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CpSignature(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return GaMultivectorProductUtils.IsZeroECp(id1, id2)
                ? 0
                : GpSignature(id1, id2);
        }
    }
}
