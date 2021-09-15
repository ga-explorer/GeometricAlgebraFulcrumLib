using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
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
            => Signature.VSpaceDimension;

        public override IGeometricAlgebraSignature Signature { get; }

        public override bool IsEuclidean 
            => false;

        public override bool IsOrthonormal 
            => false;

        public override bool IsChangeOfBasis
            => true;

        public override KVectorStorage<T> PseudoScalar { get; }
        
        public override KVectorStorage<T> PseudoScalarInverse { get; }
        
        public override KVectorStorage<T> PseudoScalarReverse { get; }


        internal GeometricAlgebraChangeOfBasisProcessor([NotNull] IGeometricAlgebraSignature signature, [NotNull] IOutermorphism<T> omTargetToOrthonormal, [NotNull] IOutermorphism<T> omOrthonormalToTarget) 
            : base(omTargetToOrthonormal.LinearProcessor)
        {
            Signature = signature;
            OmTargetToOrthonormal = omTargetToOrthonormal;
            OmOrthonormalToTarget = omOrthonormalToTarget;

            var sourcePseudoScalar =
                ScalarProcessor.CreatePseudoScalarStorage(Signature.VSpaceDimension);

            PseudoScalar = OmOrthonormalToTarget.OmMapKVector(sourcePseudoScalar);
            PseudoScalarInverse = ScalarProcessor.BladeInverse(Signature, PseudoScalar);
            PseudoScalarReverse = ScalarProcessor.Reverse(PseudoScalar).GetKVectorPart(PseudoScalar.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            return ScalarProcessor.Sp(Signature, s1, s2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Lcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Rcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Hip(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Fdp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Acp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Cp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Sp(IMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.Sp(Signature, s1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T NormSquared(IMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.NormSquared(Signature, s1);
        }
    }
}