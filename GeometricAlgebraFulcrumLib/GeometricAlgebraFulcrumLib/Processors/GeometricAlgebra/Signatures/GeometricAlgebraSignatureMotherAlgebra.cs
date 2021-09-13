using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures
{
    public sealed class GeometricAlgebraSignatureMotherAlgebra :
        IGeometricAlgebraSignatureComputed
    {
        private readonly uint _negativeMask;

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


        internal GeometricAlgebraSignatureMotherAlgebra(uint vSpaceDimension)
        {
            if (vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension || vSpaceDimension.IsOdd())
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

            var id = BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2);

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
                return 1;

            var id = BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2);

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

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            return (negativeBasisCount & 1) == 0 
                ? BasisBladeProductUtils.EGpSignature(id, id)
                : -BasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(BasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return GetBasisBladeSignature(id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSignature(id);

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
                (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSignature(id1, id2);

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
                (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                BasisBladeProductUtils.EGpReverseSignature(id1, id2);

            return (negativeBasisCount & 1) == 0 
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.OpSignature(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var negativeBasisCount = 
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                BasisBladeProductUtils.EGpSignature(id);

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
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
                (id & _negativeMask).BasisBladeIdToGrade();

            var euclideanSignature = 
                BasisBladeProductUtils.ENormSquaredSignature(id);

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
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
            if (BasisBladeProductUtils.IsNegativeEGp(id1, id2) == BasisBladeProductUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

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
            if (BasisBladeProductUtils.IsNegativeEGp(id1, id2) != BasisBladeProductUtils.IsNegativeEGp(id2, id1))
            {
                var commonBasisBladesId = id1 & id2;

                var negativeBasisCount = 
                    (commonBasisBladesId & _negativeMask).BasisBladeIdToGrade();

                var euclideanSignature = 
                    BasisBladeProductUtils.EGpSignature(id1, id2);

                return (negativeBasisCount & 1) == 0 
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }
    }
}