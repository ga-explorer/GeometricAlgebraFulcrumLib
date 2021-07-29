using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Generic
{
    public sealed class GaProcessorGenericEuclidean<T>
        : GaProcessorGenericBase<T>, IGaProcessorEuclidean<T>
    {
        public override uint VSpaceDimension { get; }

        public override IGaSignature Signature 
            => this;

        public override bool IsOrthonormal 
            => true;

        public override bool IsChangeOfBasis 
            => false;

        public uint SignatureId 
            => VSpaceDimension;

        public uint PositiveCount 
            => VSpaceDimension;

        public uint NegativeCount 
            => 0U;

        public uint ZeroCount 
            => 0U;

        public bool IsEuclidean 
            => true;

        public bool IsProjective 
            => false;

        public bool IsConformal 
            => false;

        public bool IsMotherAlgebra 
            => false;

        public override IGasKVector<T> PseudoScalar { get; }

        public override IGasKVector<T> PseudoScalarInverse { get; }

        public override IGasKVector<T> PseudoScalarReverse { get; }


        internal GaProcessorGenericEuclidean(IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(scalarProcessor)
        {
            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;

            PseudoScalar = ScalarProcessor.CreatePseudoScalar(Signature.VSpaceDimension
            );

            PseudoScalarInverse = 
                Signature
                    .BladeInverse(PseudoScalar)
                    .GetKVectorPart(Signature.VSpaceDimension);

            PseudoScalarReverse = 
                PseudoScalar
                    .GetReverse()
                    .GetKVectorPart(Signature.VSpaceDimension);
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
            var id = GaBasisUtils.BasisBladeId(grade, index);

            Debug.Assert(id < GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id, id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.EGpSignature(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.EGpReverseSignature(id1, id2);
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

            return GaBasisUtils.EGpSignature(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.ESpSignature(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisUtils.ENormSquaredSignature(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.ELcpSignature(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.ERcpSignature(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.EFdpSignature(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.EHipSignature(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            return GaBasisUtils.EAcpSignature(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            return GaBasisUtils.ECpSignature(id1, id2);
        }


        public override T Sp(IGasMultivector<T> mv1)
        {
            return mv1.ESp();
        }

        public override T Sp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.ESp(mv2);
        }

        public override IGasMultivector<T> Lcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.ELcp(mv2);
        }

        public override IGasMultivector<T> Rcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.ERcp(mv2);
        }

        public override IGasMultivector<T> Hip(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.EHip(mv2);
        }

        public override IGasMultivector<T> Fdp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.EFdp(mv2);
        }

        public override IGasMultivector<T> Acp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.EAcp(mv2);
        }

        public override IGasMultivector<T> Cp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1.ECp(mv2);
        }

        public override T NormSquared(IGasMultivector<T> mv1)
        {
            return mv1.ENormSquared();
        }

    }
}