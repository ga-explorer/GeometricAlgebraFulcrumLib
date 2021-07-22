using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public sealed class GaSignatureMotherAlgebra :
        IGaSignatureComputed
    {
        private readonly uint _negativeMask;

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public uint SignatureId 
            => PositiveCount | (NegativeCount << 6);

        public uint PositiveCount 
            => VSpaceDimension >> 1;

        public uint NegativeCount 
            => VSpaceDimension >> 1;

        public uint ZeroCount 
            => 0;

        public bool IsEuclidean => 
            false;

        public bool IsProjective 
            => false;

        public bool IsConformal 
            => false;

        public bool IsMotherAlgebra 
            => true;

        public IReadOnlyList<int> BasisVectorSignatures { get; }


        internal GaSignatureMotherAlgebra(uint vSpaceDimension)
        {
            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension || vSpaceDimension.IsOdd())
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
            var positiveCount = (int) vSpaceDimension >> 1;

            _negativeMask = 
                UInt32BitUtils.CreateMask(positiveCount) << positiveCount;

            BasisVectorSignatures = 
                Enumerable
                    .Repeat(1, positiveCount)
                    .Concat(Enumerable.Repeat(-1, positiveCount))
                    .ToArray();
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            return index < PositiveCount ? 1 : -1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            return index < PositiveCount ? 1 : 0;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < VSpaceDimension);

            if (index1 == index2)
                return 1;

            var id = (1UL << index1) | (1UL << index2);

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 
                ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            if (index1 == index2)
                return 1;

            var id = (1UL << (int)index1) | (1UL << (int)index2);

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 ? 1 : -1;
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeGrade();

            return (negativeBasisCount & 1) == 0 
                ? GaBasisUtils.EGpSignature(id, id)
                : -GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.OpSignature(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 == id2)
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id < GaSpaceDimension);

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0))
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            if (GaBasisUtils.IsNegativeEGp(id1, id2) == GaBasisUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

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
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            if (GaBasisUtils.IsNegativeEGp(id1, id2) != GaBasisUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

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