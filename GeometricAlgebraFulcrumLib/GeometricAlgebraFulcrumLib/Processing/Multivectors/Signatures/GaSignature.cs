using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures
{
    public sealed class GaSignature :
        IGaSignatureComputed
    {
        private readonly ulong _positiveMask;
        private readonly ulong _negativeMask;
        private readonly ulong _zeroMask;


        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public uint SignatureId 
            => PositiveCount | (NegativeCount << 6) | (ZeroCount << 12);

        public uint PositiveCount { get; }

        public uint NegativeCount { get; }

        public uint ZeroCount { get; }

        public bool IsEuclidean => 
            NegativeCount + ZeroCount == 0;

        public bool IsProjective 
            => NegativeCount == 0 && ZeroCount == 1;

        public bool IsConformal 
            => NegativeCount == 2 && ZeroCount == 0;

        public bool IsMotherAlgebra 
            => PositiveCount == NegativeCount && ZeroCount == 0;

        public IReadOnlyList<int> BasisVectorSignatures { get; }


        internal GaSignature(uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var vSpaceDimension = 
                positiveCount + negativeCount + zeroCount;

            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException();

            PositiveCount = positiveCount;
            NegativeCount = negativeCount;
            ZeroCount = zeroCount;
            VSpaceDimension = vSpaceDimension;

            _positiveMask = PositiveCount > 0 
                ? UInt64BitUtils.CreateMask((int) PositiveCount)
                : 0UL;

            _negativeMask = NegativeCount > 0
                ? UInt64BitUtils.CreateMask((int) NegativeCount) << (int) PositiveCount
                : 0UL ;

            _zeroMask = ZeroCount > 0
                ? UInt64BitUtils.CreateMask((int) ZeroCount) << (int) (PositiveCount + NegativeCount)
                : 0UL;

            BasisVectorSignatures = ((ulong)VSpaceDimension)
                .GetRange()
                .Select(GetBasisVectorSignature)
                .ToArray();
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            if (index < PositiveCount)
                return 1;

            return index - PositiveCount < NegativeCount 
                ? -1 : 0;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            if (index < PositiveCount)
                return 1;
            
            return (int) index - PositiveCount < NegativeCount 
                ? -1 : 0;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < VSpaceDimension);

            if (index1 == index2)
                return (index1.BasisVectorIndexToId() & _zeroMask) != 0UL ? 0 : 1;

            var id = GaBasisBivectorUtils.BasisBivectorId(index1, index2);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            return (negativeBasisCount & 1) == 0 
                ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            if (index1 == index2)
                return (index1.BasisVectorIndexToId() & _zeroMask) != 0UL ? 0 : 1;

            var id = GaBasisBivectorUtils.BasisBivectorId(index1, index2);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            return (negativeBasisCount & 1) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id, id)
                : -GaBasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(GaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                GaBasisBladeProductUtils.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                GaBasisBladeProductUtils.EGpSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

            if ((commonBasisBladesId & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                GaBasisBladeProductUtils.EGpReverseSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.OpSignature(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                GaBasisBladeProductUtils.EGpSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 == id2)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            if ((id & _zeroMask) != 0UL)
                return 0;

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                GaBasisBladeProductUtils.ENormSquaredSignature(id);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            if (GaBasisBladeProductUtils.IsNegativeEGp(id1, id2) == GaBasisBladeProductUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            if (GaBasisBladeProductUtils.IsNegativeEGp(id1, id2) != GaBasisBladeProductUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                if ((commonBasisBladesId & _zeroMask) != 0UL)
                    return 0;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    GaBasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }
    }
}
