using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
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

        public override KVector<T> PseudoScalar { get; }

        public override KVector<T> PseudoScalarInverse { get; }

        public override KVector<T> PseudoScalarReverse { get; }


        internal GeometricAlgebraOrthonormalProcessor(IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] BasisBladeSet basisSet) 
            : base(scalarProcessor)
        {
            BasisSet = basisSet;

            var pseudoScalar = 
                ScalarProcessor
                    .CreateKVectorStoragePseudoScalar(basisSet.VSpaceDimension)
                    .CreateKVector(this);
            
            PseudoScalar = pseudoScalar;
            PseudoScalarInverse = pseudoScalar.Inverse();
            PseudoScalarReverse = pseudoScalar.Reverse();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(int index)
        {
            return BasisSet.GetBasisVectorSignature(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(ulong index)
        {
            return BasisSet.GetBasisVectorSignature(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(int index1, int index2)
        {
            return BasisSet.GetBasisBivectorSignature(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            return BasisSet.GetBasisBivectorSignature(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            return BasisSet.GetBasisBladeSignature(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(ulong id)
        {
            return BasisSet.GetBasisBladeSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(BasisBlade basisBlade)
        {
            return BasisSet.GetBasisBladeSignature(basisBlade.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSquaredSign(ulong id)
        {
            return BasisSet.GpSquaredSign(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSign(ulong id1, ulong id2)
        {
            return BasisSet.GpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSign(ulong id1, ulong id2)
        {
            return BasisSet.GpReverseSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int OpSign(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.OpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSign(ulong id)
        {
            return BasisSet.SpSquaredSign(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSign(ulong id1, ulong id2)
        {
            return BasisSet.SpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NormSquaredSign(ulong id)
        {
            return BasisSet.NormSquaredSign(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LcpSign(ulong id1, ulong id2)
        {
            return BasisSet.LcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RcpSign(ulong id1, ulong id2)
        {
            return BasisSet.RcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FdpSign(ulong id1, ulong id2)
        {
            return BasisSet.FdpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HipSign(ulong id1, ulong id2)
        {
            return BasisSet.HipSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AcpSign(ulong id1, ulong id2)
        {
            return BasisSet.AcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CpSign(ulong id1, ulong id2)
        {
            return BasisSet.CpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T SpSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Sp(BasisSet, mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Sp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Lcp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Rcp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Hip(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Fdp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Acp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.Cp(BasisSet, mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.NormSquared(BasisSet, mv1);
        }
    }
}