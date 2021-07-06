using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Signatures
{
    public readonly struct GaSignatureConformal :
        IGaSignature
    {
        public static GaSignatureConformal Create(int euclideanVSpaceDimension)
        {
            return new GaSignatureConformal(euclideanVSpaceDimension);
        }


        public GaBasisSet BasisSet { get; }

        public int SignatureId { get; }
        
        public int VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public int PositiveCount 
            => BasisSet.VSpaceDimension - 1;

        public int NegativeCount 
            => 1;

        public int ZeroCount 
            => 0;
        
        public bool IsEuclidean 
            => false;

        public int EuclideanVSpaceDimension 
            => BasisSet.VSpaceDimension - 2;

        public ulong NegativeBasisVectorIndex 
            => (ulong) BasisSet.VSpaceDimension - 1;

        public ulong NegativeBasisVectorId 
            => 1UL << (BasisSet.VSpaceDimension - 1);


        private GaSignatureConformal(int euclideanVSpaceDimension)
        {
            var vSpaceDimension = euclideanVSpaceDimension + 2;

            if (vSpaceDimension < 2 || vSpaceDimension > GaBasisUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(euclideanVSpaceDimension));

            BasisSet = new GaBasisSet(vSpaceDimension);
            SignatureId = (vSpaceDimension - 1) | (1 << 6);
        }

        
        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < BasisSet.VSpaceDimension);

            return (ulong) index == NegativeBasisVectorIndex 
                ? -1 : 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < (ulong) BasisSet.VSpaceDimension);

            return index == NegativeBasisVectorIndex 
                ? -1 : 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 < BasisSet.VSpaceDimension);
            Debug.Assert(index2 < BasisSet.VSpaceDimension);

            return (ulong) index1 < NegativeBasisVectorIndex && 
                   (ulong) index2 < NegativeBasisVectorIndex
                ? -1 : 1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < (ulong) BasisSet.VSpaceDimension);
            Debug.Assert(index2 < (ulong) BasisSet.VSpaceDimension);

            return index1 < NegativeBasisVectorIndex && 
                   index2 < NegativeBasisVectorIndex
                ? -1 : 1;
        }

        public int GetBasisBladeSignature(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id, id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpSignature(id1, id2);

            return (id1 & id2 & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            var euclideanSignature = 
                GaBasisUtils.EGpReverseSignature(id1, id2);

            return (id1 & id2 & NegativeBasisVectorId) == 0
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

            var euclideanSignature =
                GaBasisUtils.EGpSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            var euclideanSignature =
                GaBasisUtils.ENormSquaredSignature(id);

            return (id & NegativeBasisVectorId) == 0
                ? euclideanSignature
                : -euclideanSignature;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

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