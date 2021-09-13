using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public sealed class GeometricAlgebraOrthonormalProcessor<T> : 
        GeometricAlgebraProcessorBase<T>, 
        IGeometricAlgebraOrthonormalProcessor<T>
    {
        public override uint VSpaceDimension 
            => Signature.VSpaceDimension;

        public override IGeometricAlgebraSignature Signature { get; }

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
        
        public override bool IsEuclidean 
            => Signature.IsEuclidean;
        
        public bool IsProjective 
            => Signature.IsProjective;
        
        public bool IsConformal 
            => Signature.IsConformal;
        
        public bool IsMotherAlgebra 
            => Signature.IsMotherAlgebra;

        public override KVectorStorage<T> PseudoScalar { get; }

        public override KVectorStorage<T> PseudoScalarInverse { get; }

        public override KVectorStorage<T> PseudoScalarReverse { get; }


        internal GeometricAlgebraOrthonormalProcessor(IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IGeometricAlgebraSignature signature) 
            : base(scalarProcessor)
        {
            Signature = signature;

            PseudoScalar = ScalarProcessor.CreatePseudoScalarStorage(Signature.VSpaceDimension);

            PseudoScalarInverse = 
                scalarProcessor
                    .BladeInverse(Signature, PseudoScalar)
                    .GetKVectorPart(Signature.VSpaceDimension);

            PseudoScalarReverse = 
                scalarProcessor
                    .Reverse(PseudoScalar)
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

        public int GetBasisBladeSignature(BasisBlade basisBlade)
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

            return BasisBladeProductUtils.OpSignature(id1, id2);
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

        public override T Sp(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Sp(Signature, mv1);
        }

        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Sp(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Lcp(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Rcp(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Hip(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Fdp(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Acp(Signature, mv1, mv2);
        }

        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Cp(Signature, mv1, mv2);
        }

        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.NormSquared(Signature, mv1);
        }
    }
}