using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public sealed class GaProcessorChangeOfBasis<T> : 
        GaProcessorBase<T>, 
        IGaProcessorChangeOfBasis<T>
    {
        public IGaOutermorphism<T> OmTargetToOrthonormal { get; }

        public IGaOutermorphism<T> OmOrthonormalToTarget { get; }

        public override uint VSpaceDimension 
            => OmOrthonormalToTarget.VSpaceDimension;

        public override IGaSignature Signature { get; }

        public override bool IsOrthonormal 
            => false;

        public override bool IsChangeOfBasis
            => true;

        public override IGaKVectorStorage<T> PseudoScalar { get; }
        
        public override IGaKVectorStorage<T> PseudoScalarInverse { get; }
        
        public override IGaKVectorStorage<T> PseudoScalarReverse { get; }


        internal GaProcessorChangeOfBasis([NotNull] IGaSignature signature, [NotNull] IGaOutermorphism<T> omTargetToOrthonormal, [NotNull] IGaOutermorphism<T> omOrthonormalToTarget) 
            : base(omTargetToOrthonormal.ScalarsGridProcessor)
        {
            Signature = signature;
            OmTargetToOrthonormal = omTargetToOrthonormal;
            OmOrthonormalToTarget = omOrthonormalToTarget;

            var sourcePseudoScalar =
                ScalarProcessor.CreatePseudoScalarStorage(Signature.VSpaceDimension);

            PseudoScalar = OmOrthonormalToTarget.MapKVector(sourcePseudoScalar);
            PseudoScalarInverse = ScalarProcessor.BladeInverse(Signature, PseudoScalar);
            PseudoScalarReverse = ScalarProcessor.Reverse(PseudoScalar).GetKVectorPart(PseudoScalar.Grade);
        }



        public override T Sp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            return ScalarProcessor.Sp(Signature, s1, s2);
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Lcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Rcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Hip(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Fdp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Acp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Cp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override T Sp(IGaMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.Sp(Signature, s1);
        }

        public override T NormSquared(IGaMultivectorStorage<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.NormSquared(Signature, s1);
        }
    }
}