using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public sealed class GeometricAlgebraChangeOfBasisProcessor<T> : 
        GeometricAlgebraProcessorBase<T>, 
        IGeometricAlgebraChangeOfBasisProcessor<T>
    {
        public IOutermorphism<T> OmTargetToOrthonormal { get; }

        public IOutermorphism<T> OmOrthonormalToTarget { get; }

        public override uint VSpaceDimension 
            => BasisSet.VSpaceDimension;

        public override BasisBladeSet BasisSet { get; }

        public override bool IsEuclidean 
            => false;

        public override bool IsOrthonormal 
            => false;

        public override bool IsChangeOfBasis
            => true;

        public override KVectorStorage<T> PseudoScalar { get; }
        
        public override KVectorStorage<T> PseudoScalarInverse { get; }
        
        public override KVectorStorage<T> PseudoScalarReverse { get; }


        internal GeometricAlgebraChangeOfBasisProcessor([NotNull] BasisBladeSet basisSet, [NotNull] IOutermorphism<T> omTargetToOrthonormal, [NotNull] IOutermorphism<T> omOrthonormalToTarget) 
            : base(omTargetToOrthonormal.LinearProcessor)
        {
            BasisSet = basisSet;
            OmTargetToOrthonormal = omTargetToOrthonormal;
            OmOrthonormalToTarget = omOrthonormalToTarget;

            var sourcePseudoScalar =
                ScalarProcessor.CreatePseudoScalarStorage(BasisSet.VSpaceDimension);

            PseudoScalar = OmOrthonormalToTarget.OmMapKVector(sourcePseudoScalar);
            PseudoScalarInverse = ScalarProcessor.BladeInverse(BasisSet, PseudoScalar);
            PseudoScalarReverse = ScalarProcessor.Reverse(PseudoScalar).GetKVectorPart(PseudoScalar.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            return ScalarProcessor.Sp(BasisSet, s1, s2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Lcp(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Rcp(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Hip(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Fdp(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Acp(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Cp(BasisSet, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.Sp(BasisSet, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.NormSquared(BasisSet, s1);
        }
    }
}