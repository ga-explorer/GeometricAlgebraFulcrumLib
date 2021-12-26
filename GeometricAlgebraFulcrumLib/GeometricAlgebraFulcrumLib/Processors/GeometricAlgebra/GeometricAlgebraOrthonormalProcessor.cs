using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public class GeometricAlgebraOrthonormalProcessor<T> : 
        GeometricAlgebraProcessorBase<T>, 
        IGeometricAlgebraOrthonormalProcessor<T>
    {
        public override uint VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public override BasisBladeSet BasisSet { get; }

        public override bool IsOrthonormal
            => true;

        public override bool IsChangeOfBasis 
            => false;

        public Triplet<ulong> SignatureId 
            => BasisSet.BasisSetSignature;

        public uint PositiveCount 
            => BasisSet.PositiveCount;
        
        public uint NegativeCount 
            => BasisSet.NegativeCount;
        
        public uint ZeroCount 
            => BasisSet.ZeroCount;
        
        public override bool IsEuclidean 
            => BasisSet.IsEuclidean;
        
        public bool IsProjective 
            => BasisSet.IsProjective;
        
        public bool IsConformal 
            => BasisSet.IsConformal;
        
        public bool IsMotherAlgebra 
            => BasisSet.IsMotherAlgebra;

        public override KVectorStorage<T> PseudoScalar { get; }

        public override KVectorStorage<T> PseudoScalarInverse { get; }

        public override KVectorStorage<T> PseudoScalarReverse { get; }


        internal GeometricAlgebraOrthonormalProcessor(IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] BasisBladeSet basisSet) 
            : base(scalarProcessor)
        {
            BasisSet = basisSet;

            var pseudoScalar = 
                ScalarProcessor.CreatePseudoScalarStorage(basisSet.VSpaceDimension);

            PseudoScalar = pseudoScalar;

            PseudoScalarInverse = 
                scalarProcessor
                    .BladeInverse(basisSet, pseudoScalar)
                    .GetKVectorPart(basisSet.VSpaceDimension);

            PseudoScalarReverse = 
                scalarProcessor
                    .Reverse(pseudoScalar)
                    .GetKVectorPart(basisSet.VSpaceDimension);
        }


        public int GetBasisVectorSignature(int index)
        {
            return BasisSet.GetBasisVectorSignature(index);
        }

        public int GetBasisVectorSignature(ulong index)
        {
            return BasisSet.GetBasisVectorSignature(index);
        }

        public int GetBasisBivectorSignature(int index1, int index2)
        {
            return BasisSet.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            return BasisSet.GetBasisBivectorSignature(index1, index2);
        }

        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            return BasisSet.GetBasisBladeSignature(grade, index);
        }

        public int GetBasisBladeSignature(ulong id)
        {
            return BasisSet.GetBasisBladeSignature(id);
        }

        public int GetBasisBladeSignature(BasisBlade basisBlade)
        {
            return BasisSet.GetBasisBladeSignature(basisBlade.Id);
        }

        public int GpSignature(ulong id)
        {
            return BasisSet.GpSquaredSign(id);
        }

        public int GpSignature(ulong id1, ulong id2)
        {
            return BasisSet.GpSign(id1, id2);
        }

        public int GpReverseSignature(ulong id1, ulong id2)
        {
            return BasisSet.GpReverseSign(id1, id2);
        }

        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.OpSign(id1, id2);
        }

        public int SpSignature(ulong id)
        {
            return BasisSet.SpSquaredSign(id);
        }

        public int SpSignature(ulong id1, ulong id2)
        {
            return BasisSet.SpSign(id1, id2);
        }

        public int NormSquaredSignature(ulong id)
        {
            return BasisSet.NormSquaredSign(id);
        }

        public int LcpSignature(ulong id1, ulong id2)
        {
            return BasisSet.LcpSign(id1, id2);
        }

        public int RcpSignature(ulong id1, ulong id2)
        {
            return BasisSet.RcpSign(id1, id2);
        }

        public int FdpSignature(ulong id1, ulong id2)
        {
            return BasisSet.FdpSign(id1, id2);
        }

        public int HipSignature(ulong id1, ulong id2)
        {
            return BasisSet.HipSign(id1, id2);
        }

        public int AcpSignature(ulong id1, ulong id2)
        {
            return BasisSet.AcpSign(id1, id2);
        }

        public int CpSignature(ulong id1, ulong id2)
        {
            return BasisSet.CpSign(id1, id2);
        }

        public override T Sp(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Sp(BasisSet, mv1);
        }

        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Sp(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Lcp(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Rcp(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Hip(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Fdp(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Acp(BasisSet, mv1, mv2);
        }

        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Cp(BasisSet, mv1, mv2);
        }

        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.NormSquared(BasisSet, mv1);
        }
    }
}