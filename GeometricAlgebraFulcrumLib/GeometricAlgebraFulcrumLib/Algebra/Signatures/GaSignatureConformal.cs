using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public readonly struct GaSignatureConformal :
        IGaSignatureComputed
    {
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
            => (VSpaceDimension - 1) | (1 << 6);
        
        public uint PositiveCount 
            => VSpaceDimension - 1;

        public uint NegativeCount 
            => 1;

        public uint ZeroCount 
            => 0;
        
        public bool IsEuclidean 
            => false;

        public bool IsProjective 
            => false;

        public bool IsConformal 
            => true;

        public bool IsMotherAlgebra 
            => false;

        public uint EuclideanVSpaceDimension 
            => VSpaceDimension - 2;

        public ulong NegativeBasisVectorIndex 
            => (ulong) VSpaceDimension - 1;

        public ulong NegativeBasisVectorId 
            => 1UL << (int) (VSpaceDimension - 1);


        internal GaSignatureConformal(uint vSpaceDimension)
        {
            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
        }

        
        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            return (ulong) index == NegativeBasisVectorIndex 
                ? -1 : 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            return index == NegativeBasisVectorIndex 
                ? -1 : 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            return (ulong) index1 < NegativeBasisVectorIndex && 
                   (ulong) index2 < NegativeBasisVectorIndex
                ? -1 : 1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            return index1 < NegativeBasisVectorIndex && 
                   index2 < NegativeBasisVectorIndex
                ? -1 : 1;
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id1, id2);

            return (id1 & id2 & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpReverseSignature(id1, id2);

            return (id1 & id2 & NegativeBasisVectorId) == 0
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

            var euclideanSignature =
                GaBasisUtils.EGpSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 == id2)
            {
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            var euclideanSignature =
                GaBasisUtils.ENormSquaredSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
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
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
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
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
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
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
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
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
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
                var euclideanSignature =
                    GaBasisUtils.EGpSignature(id1, id2);

                return (id1 & id2 & NegativeBasisVectorId) == 0
                    ? euclideanSignature
                    : -euclideanSignature;
            }

            return 0;
        }
    }
}