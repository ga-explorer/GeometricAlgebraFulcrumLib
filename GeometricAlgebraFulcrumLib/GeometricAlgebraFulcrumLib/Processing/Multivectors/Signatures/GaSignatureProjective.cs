using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures
{
    /// <summary>
    /// Represents the signature of a PGA basis (p,0,1)
    /// </summary>
    public sealed record GaSignatureProjective :
        IGaSignatureComputed
    {
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
            => (VSpaceDimension - 1) | (1u << 12);

        public uint PositiveCount 
            => VSpaceDimension - 1;

        public uint NegativeCount 
            => 0;

        public uint ZeroCount 
            => 1;
        
        public bool IsEuclidean 
            => false;

        public bool IsProjective 
            => true;

        public bool IsConformal 
            => false;

        public bool IsMotherAlgebra 
            => false;

        public uint EuclideanVSpaceDimension 
            => VSpaceDimension - 1;

        public ulong ZeroBasisVectorIndex 
            => (ulong) VSpaceDimension - 2;

        public ulong ZeroBasisVectorId 
            => (VSpaceDimension - 2).BasisVectorIndexToId();


        internal GaSignatureProjective(uint vSpaceDimension)
        {
            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            return (ulong) index == ZeroBasisVectorIndex 
                ? 0 : 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            return index == ZeroBasisVectorIndex 
                ? 0 : 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < VSpaceDimension);

            if ((ulong) index1 == ZeroBasisVectorIndex || (ulong) index2 == ZeroBasisVectorIndex)
                return 0;

            return index1 == index2 
                ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            if (index1 == ZeroBasisVectorIndex || index2 == ZeroBasisVectorIndex)
                return 0;

            return index1 == index2 
                ? 1 : -1;
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            Debug.Assert(id < GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GetBasisBladeSignature(GaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id, id) 
                : 0;
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id) 
                : 0;
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return (id1 & id2 & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                : 0;
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return (id1 & id2 & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpReverseSignature(id1, id2) 
                : 0;
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

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.EGpSignature(id) 
                : 0;
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 == id2)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return (id & ZeroBasisVectorId) == 0 
                ? GaBasisBladeProductUtils.ENormSquaredSignature(id) 
                : 0;
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id2 & ~id1) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            if (id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0))
            {
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
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
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
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
                return (id1 & id2 & ZeroBasisVectorId) == 0 
                    ? GaBasisBladeProductUtils.EGpSignature(id1, id2) 
                    : 0;
            }

            return 0;
        }
    }
}