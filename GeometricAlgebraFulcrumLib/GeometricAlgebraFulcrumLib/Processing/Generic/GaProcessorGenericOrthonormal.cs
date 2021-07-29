using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Generic
{
    public sealed class GaProcessorGenericOrthonormal<T>
        : GaProcessorGenericBase<T>, IGaProcessorOrthonormal<T>
    {
        public override uint VSpaceDimension 
            => Signature.VSpaceDimension;

        public override IGaSignature Signature { get; }

        public override bool IsOrthonormal
            => true;

        public override bool IsChangeOfBasis 
            => false;

        public uint SignatureId 
            => Signature.SignatureId;

        public uint PositiveCount 
            => Signature.PositiveCount;
        
        public uint NegativeCount 
            => Signature.NegativeCount;
        
        public uint ZeroCount 
            => Signature.ZeroCount;
        
        public bool IsEuclidean 
            => Signature.IsEuclidean;
        
        public bool IsProjective 
            => Signature.IsProjective;
        
        public bool IsConformal 
            => Signature.IsConformal;
        
        public bool IsMotherAlgebra 
            => Signature.IsMotherAlgebra;

        public override IGasKVector<T> PseudoScalar { get; }

        public override IGasKVector<T> PseudoScalarInverse { get; }

        public override IGasKVector<T> PseudoScalarReverse { get; }


        internal GaProcessorGenericOrthonormal(IGaScalarProcessor<T> scalarProcessor, [NotNull] IGaSignature signature) 
            : base(scalarProcessor)
        {
            Signature = signature;

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
            return Signature.GetBasisVectorSignature(index);
        }

        public int GetBasisVectorSignature(ulong index)
        {
            return Signature.GetBasisVectorSignature(index);
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            return Signature.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            return Signature.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            return Signature.GetBasisBladeSignature(grade, index);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            return Signature.GetBasisBladeSignature(id);
        }

        public int GetBasisBladeSignature(IGaBasisBlade basisBlade)
        {
            return Signature.GetBasisBladeSignature(basisBlade);
        }

        public int GpSignature(ulong id)
        {
            return Signature.GpSignature(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            return Signature.GpSignature(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            return Signature.GpReverseSignature(id1, id2);
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return GaBasisUtils.OpSignature(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            return Signature.SpSignature(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            return Signature.SpSignature(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            return Signature.NormSquaredSignature(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            return Signature.LcpSignature(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            return Signature.RcpSignature(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            return Signature.FdpSignature(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            return Signature.HipSignature(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            return Signature.AcpSignature(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            return Signature.CpSignature(id1, id2);
        }

        public override T Sp(IGasMultivector<T> mv1)
        {
            return Signature.Sp(mv1);
        }

        public override T Sp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Sp(mv1, mv2);
        }

        public override IGasMultivector<T> Lcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Lcp(mv1, mv2);
        }

        public override IGasMultivector<T> Rcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Rcp(mv1, mv2);
        }

        public override IGasMultivector<T> Hip(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Hip(mv1, mv2);
        }

        public override IGasMultivector<T> Fdp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Fdp(mv1, mv2);
        }

        public override IGasMultivector<T> Acp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Acp(mv1, mv2);
        }

        public override IGasMultivector<T> Cp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return Signature.Cp(mv1, mv2);
        }

        public override T NormSquared(IGasMultivector<T> mv1)
        {
            return Signature.NormSquared(mv1);
        }
    }
}