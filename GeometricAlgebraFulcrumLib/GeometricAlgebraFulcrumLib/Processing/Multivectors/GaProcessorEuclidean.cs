using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public sealed class GaProcessorEuclidean<T> : 
        GaProcessorBase<T>, 
        IGaProcessorEuclidean<T>
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

        public override IGaKVectorStorage<T> PseudoScalar { get; }

        public override IGaKVectorStorage<T> PseudoScalarInverse { get; }

        public override IGaKVectorStorage<T> PseudoScalarReverse { get; }


        internal GaProcessorEuclidean(IScalarProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(scalarProcessor)
        {
            if (vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;

            PseudoScalar = ScalarProcessor.CreatePseudoScalarStorage(Signature.VSpaceDimension);

            PseudoScalarInverse = 
                ScalarProcessor
                    .BladeInverse(Signature, PseudoScalar)
                    .GetKVectorPart(Signature.VSpaceDimension);

            PseudoScalarReverse = 
                scalarProcessor
                    .Reverse(PseudoScalar)
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
            var id = index.BasisBladeIndexToId(grade);

            Debug.Assert(id < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GetBasisBladeSignature(GaBasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpSignature(id, id);
        }

        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpSignature(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpSignature(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.EGpReverseSignature(id1, id2);
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

            return GaBasisBladeProductUtils.EGpSignature(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.ESpSignature(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return GaBasisBladeProductUtils.ENormSquaredSignature(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.ELcpSignature(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.ERcpSignature(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.EFdpSignature(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisBladeProductUtils.EHipSignature(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            return GaBasisBladeProductUtils.EAcpSignature(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            return GaBasisBladeProductUtils.ECpSignature(id1, id2);
        }


        public override T Sp(IGaMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.ESp(mv1);
        }

        public override T Sp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ESp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ELcp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ERcp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EHip(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EFdp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EAcp(mv1, mv2);
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ECp(mv1, mv2);
        }

        public override T NormSquared(IGaMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.ENormSquared(mv1);
        }

    }
}