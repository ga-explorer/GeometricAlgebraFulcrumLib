using System;
using System.Diagnostics;
using GeometricAlgebraLib.Algebra.Basis;

namespace GeometricAlgebraLib.Algebra.Signatures
{
    /// <summary>
    /// Represents the signature of a EGA basis (n,0,0)
    /// </summary>
    public readonly struct GaSignatureEuclidean :
        IGaSignature
    {
        public static GaSignatureEuclidean Create()
        {
            return new GaSignatureEuclidean(GaBasisUtils.MaxVSpaceDimension);
        }

        public static GaSignatureEuclidean Create(int vSpaceDimension)
        {
            return new GaSignatureEuclidean(vSpaceDimension);
        }


        public GaBasisSet BasisSet { get; }

        public int SignatureId 
            => BasisSet.VSpaceDimension;

        public int VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public int PositiveCount 
            => BasisSet.VSpaceDimension;

        public int NegativeCount 
            => 0;

        public int ZeroCount 
            => 0;

        public bool IsEuclidean 
            => true;


        private GaSignatureEuclidean(int vSpaceDimension)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaBasisUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            BasisSet = new GaBasisSet(vSpaceDimension);
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < BasisSet.VSpaceDimension);

            return 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < (ulong) BasisSet.VSpaceDimension);

            return 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < BasisSet.VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < BasisSet.VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < (ulong) BasisSet.VSpaceDimension);
            Debug.Assert(index2 < (ulong) BasisSet.VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        public int GetBasisBladeSignature(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EGpReverseSignature(id1, id2);
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

            return GaBasisUtils.EGpSignature(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.ESpSignature(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < BasisSet.GaSpaceDimension);

            return GaBasisUtils.ENormSquaredSignature(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.ELcpSignature(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.ERcpSignature(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EFdpSignature(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            return GaBasisUtils.EHipSignature(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            return GaBasisUtils.EAcpSignature(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < BasisSet.GaSpaceDimension);
            Debug.Assert(id2 < BasisSet.GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            return GaBasisUtils.ECpSignature(id1, id2);
        }
    }
}