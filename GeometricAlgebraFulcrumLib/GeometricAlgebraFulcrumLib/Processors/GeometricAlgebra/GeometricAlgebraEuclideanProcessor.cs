using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public sealed class GeometricAlgebraEuclideanProcessor<T> : 
        GeometricAlgebraProcessorBase<T>, 
        IGeometricAlgebraEuclideanProcessor<T>
    {
        public override uint VSpaceDimension { get; }

        public override BasisBladeSet BasisSet { get; }

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

        public override bool IsEuclidean 
            => true;

        public bool IsProjective 
            => false;

        public bool IsConformal 
            => false;

        public bool IsMotherAlgebra 
            => false;

        public override GaKVector<T> PseudoScalar { get; }

        public override GaKVector<T> PseudoScalarInverse { get; }

        public override GaKVector<T> PseudoScalarReverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GeometricAlgebraEuclideanProcessor(IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(scalarProcessor)
        {
            if (vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
            BasisSet = BasisBladeSet.CreateEuclidean(VSpaceDimension);
            PseudoScalar = ScalarProcessor.CreateKVectorStoragePseudoScalar(BasisSet.VSpaceDimension).CreateKVector(this);
            PseudoScalarInverse = PseudoScalar.EInverse();
            PseudoScalarReverse = PseudoScalar.Reverse();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(int index)
        {
            Debug.Assert(index >= 0 && index < VSpaceDimension);

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorSignature(ulong index)
        {
            Debug.Assert(index < VSpaceDimension);

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < VSpaceDimension);
            Debug.Assert(index2 >= 0 && index2 < VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBivectorSignature(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < VSpaceDimension);
            Debug.Assert(index2 < VSpaceDimension);

            return index1 == index2 ? 1 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSign(id, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSign(id, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisBladeSignature(BasisBlade basisBlade)
        {
            var id = basisBlade.Id;

            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSign(id, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSquaredSign(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GpReverseSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EGpReverseSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int OpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.OpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return BasisBladeProductUtils.EGpSquaredSign(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ESpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int NormSquaredSignature(ulong id)
        {
            Debug.Assert(id < GaSpaceDimension);

            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ELcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.ERcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FdpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EFdpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HipSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            return BasisBladeProductUtils.EHipSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AcpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A acp B = (AB + BA) / 2
            return BasisBladeProductUtils.EAcpSign(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CpSignature(ulong id1, ulong id2)
        {
            Debug.Assert(id1 < GaSpaceDimension);
            Debug.Assert(id2 < GaSpaceDimension);

            //A cp B = (AB - BA) / 2
            return BasisBladeProductUtils.ECpSign(id1, id2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Pair<BasisVectorFrame<T>> GetBasisVectorFrame()
        {
            var frame = BasisVectorFrame<T>.Create(this);

            return new Pair<BasisVectorFrame<T>>(frame, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T SpSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.ESpSquared(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ESp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ELcp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ERcp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EHip(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EFdp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.EAcp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return ScalarProcessor.ECp(mv1, mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.ENormSquared(mv1);
        }

    }
}