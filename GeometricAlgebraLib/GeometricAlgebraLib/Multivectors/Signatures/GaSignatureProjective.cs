using System;
using System.Diagnostics;
using GeometricAlgebraLib.Multivectors.Basis;

namespace GeometricAlgebraLib.Multivectors.Signatures
{
    /// <summary>
    /// Represents the signature of a PGA basis (p,0,1)
    /// </summary>
    public readonly struct GaSignatureProjective :
        IGaSignature
    {
        public static GaSignatureProjective Create(int euclideanVSpaceDimension)
        {
            return new GaSignatureProjective(euclideanVSpaceDimension);
        }


        public GaBasisSet BasisSet { get; }

        public int SignatureId { get; }

        public int VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public int PositiveCount 
            => BasisSet.VSpaceDimension - 1;

        public int NegativeCount 
            => 0;

        public int ZeroCount 
            => 1;

        public int EuclideanVSpaceDimension 
            => BasisSet.VSpaceDimension - 1;

        public ulong ZeroBasisVectorIndex 
            => (ulong) BasisSet.VSpaceDimension - 2;

        public ulong ZeroBasisVectorId 
            => 1UL << (BasisSet.VSpaceDimension - 2);


        private GaSignatureProjective(int euclideanVSpaceDimension)
        {
            var vSpaceDimension = euclideanVSpaceDimension + 1;

            if (vSpaceDimension < 2 || vSpaceDimension > GaBasisUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(euclideanVSpaceDimension));

            BasisSet = new GaBasisSet(vSpaceDimension);
            SignatureId = (vSpaceDimension - 1) | (1 << 12);
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < BasisSet.VSpaceDimension);

            return (ulong) index == ZeroBasisVectorIndex 
                ? 0 : 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < (ulong) BasisSet.VSpaceDimension);

            return index == ZeroBasisVectorIndex 
                ? 0 : 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < BasisSet.VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < BasisSet.VSpaceDimension);

            if ((ulong) index1 == ZeroBasisVectorIndex || (ulong) index2 == ZeroBasisVectorIndex)
                return 0;

            return index1 == index2 
                ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < (ulong) BasisSet.VSpaceDimension);
            Debug.Assert(index2 < (ulong) BasisSet.VSpaceDimension);

            if (index1 == ZeroBasisVectorIndex || index2 == ZeroBasisVectorIndex)
                return 0;

            return index1 == index2 
                ? 1 : -1;
        }

        public int GetBasisBladeSignature(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return (id1 & id2 & ZeroBasisVectorId) == 0 
                ? GaBasisUtils.EGpSignature(id1, id2) 
                : 0;
        }
        
        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return (id1 & id2) == 0
                ? GaBasisUtils.EGpSignature(id1, id2)
                : 0;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if (id1 == id2)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id2 & ~id1) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            if (id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0))
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
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
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
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
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }
    }
}