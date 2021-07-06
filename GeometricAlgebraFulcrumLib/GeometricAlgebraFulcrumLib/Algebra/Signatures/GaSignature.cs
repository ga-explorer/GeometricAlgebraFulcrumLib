using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public sealed class GaSignature :
        IGaSignature
    {
        public static IGaSignature CreateEuclidean()
        {
            return GaSignatureEuclidean.Create();
        }

        public static IGaSignature CreateEuclidean(int vSpaceDimension)
        {
            return GaSignatureEuclidean.Create(vSpaceDimension);
        }

        public static IGaSignature CreateConformal(int euclideanSpaceDimension)
        {
            return GaSignatureConformal.Create(euclideanSpaceDimension);
        }

        public static IGaSignature CreateProjective(int euclideanSpaceDimension)
        {
            return GaSignatureProjective.Create(euclideanSpaceDimension);
        }

        public static IGaSignature Create(int positiveBasisCount, int negativeBasisCount)
        {
            return new GaSignature(positiveBasisCount, negativeBasisCount, 0);
        }

        public static IGaSignature Create(int positiveBasisCount, int negativeBasisCount, int zeroBasisCount)
        {
            return new GaSignature(positiveBasisCount, negativeBasisCount, zeroBasisCount);
        }


        private readonly ulong _positiveMask;

        private readonly ulong _negativeMask;

        private readonly ulong _zeroMask;


        public GaBasisSet BasisSet { get; }

        public int VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public int SignatureId { get; }

        public int PositiveCount { get; }

        public int NegativeCount { get; }

        public int ZeroCount { get; }

        public bool IsEuclidean => 
            NegativeCount + ZeroCount == 0;

        public IReadOnlyList<int> BasisVectorSignatures { get; }


        private GaSignature(int positiveCount, int negativeCount, int zeroCount)
        {
            if (positiveCount < 0 || negativeCount < 0 || zeroCount < 0)
                throw new ArgumentOutOfRangeException();

            var vSpaceDimension = 
                positiveCount + negativeCount + zeroCount;

            if (vSpaceDimension < 2 || vSpaceDimension > GaBasisUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException();

            SignatureId = positiveCount | (negativeCount << 6) | (zeroCount << 12);
            PositiveCount = positiveCount;
            NegativeCount = negativeCount;
            ZeroCount = zeroCount;
            BasisSet = new GaBasisSet(vSpaceDimension);

            _positiveMask = PositiveCount > 0 
                ? UInt64BitUtils.CreateMask(PositiveCount)
                : 0UL;

            _negativeMask = NegativeCount > 0
                ? UInt64BitUtils.CreateMask(NegativeCount) << PositiveCount
                : 0UL ;

            _zeroMask = ZeroCount > 0
                ? UInt64BitUtils.CreateMask(ZeroCount) << (PositiveCount + NegativeCount)
                : 0UL;

            BasisVectorSignatures = Enumerable
                .Range(0, BasisSet.VSpaceDimension)
                .Select(GetBasisVectorSignature)
                .ToArray();
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < BasisSet.VSpaceDimension);

            if (index < PositiveCount)
                return 1;

            return index - PositiveCount < NegativeCount 
                ? -1 : 0;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < (ulong) BasisSet.VSpaceDimension);

            if (index < (ulong) PositiveCount)
                return 1;
            
            return (int) index - PositiveCount < NegativeCount 
                ? -1 : 0;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < BasisSet.VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < BasisSet.VSpaceDimension);

            if (index1 == index2)
                return ((1UL << index1) & _zeroMask) != 0UL ? 0 : 1;

            var id = (1UL << index1) | (1UL << index2);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 
                ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < (ulong) BasisSet.VSpaceDimension);
            Debug.Assert(index2 < (ulong) BasisSet.VSpaceDimension);

            if (index1 == index2)
                return ((1UL << (int) index1) & _zeroMask) != 0UL ? 0 : 1;

            var id = (1UL << (int)index1) | (1UL << (int)index2);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBladeSignature(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 
                ? GaBasisUtils.EGpSignature(id, id)
                : -GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (commonBasisBladesId & _negativeMask).BasisBladeGrade();

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (commonBasisBladesId & _negativeMask).BasisBladeGrade();

            var euclideanSignature = 
                GaBasisUtils.EGpReverseSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.OpSignature(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if (id1 == id2)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            var euclideanSignature = 
                GaBasisUtils.ENormSquaredSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if (id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            if (GaBasisUtils.IsNegativeEGp(id1, id2) == GaBasisUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            if (GaBasisUtils.IsNegativeEGp(id1, id2) != GaBasisUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeGrade();

                var euclideanSignature = 
                    GaBasisUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }
    }
}
