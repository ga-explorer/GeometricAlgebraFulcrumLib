using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures
{
    /// <summary>
    /// Represents the signature of a EGA basis (n,0,0)
    /// </summary>
    public sealed record GeometricAlgebraSignatureEuclidean :
        IGeometricAlgebraSignatureComputed
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
            => VSpaceDimension;

        public uint PositiveCount 
            => VSpaceDimension;

        public uint NegativeCount 
            => 0;

        public uint ZeroCount 
            => 0;

        public bool IsEuclidean 
            => true;

        public bool IsProjective 
            => false;

        public bool IsConformal 
            => false;

        public bool IsMotherAlgebra 
            => false;


        internal GeometricAlgebraSignatureEuclidean(uint vSpaceDimension)
        {
            if (vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
        }


        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            return 1;
        }

        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            return 1;
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(BasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSignature(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSignature(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpReverseSignature(id1, id2);
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

            return BasisBladeProductUtils.EGpSignature(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ESpSignature(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.ENormSquaredSignature(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ELcpSignature(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ERcpSignature(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EFdpSignature(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EHipSignature(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            return BasisBladeProductUtils.EAcpSignature(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            return BasisBladeProductUtils.ECpSignature(id1, id2);
        }
    }
}