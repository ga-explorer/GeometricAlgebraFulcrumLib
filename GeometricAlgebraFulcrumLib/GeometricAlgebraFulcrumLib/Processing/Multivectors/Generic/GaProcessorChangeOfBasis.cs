using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic
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

        public override IGaStorageKVector<T> PseudoScalar { get; }
        
        public override IGaStorageKVector<T> PseudoScalarInverse { get; }
        
        public override IGaStorageKVector<T> PseudoScalarReverse { get; }


        internal GaProcessorChangeOfBasis([NotNull] IGaSignature signature, [NotNull] IGaOutermorphism<T> omTargetToOrthonormal, [NotNull] IGaOutermorphism<T> omOrthonormalToTarget) 
            : base(omTargetToOrthonormal.ScalarsGridProcessor)
        {
            Signature = signature;
            OmTargetToOrthonormal = omTargetToOrthonormal;
            OmOrthonormalToTarget = omOrthonormalToTarget;

            var sourcePseudoScalar =
                ScalarProcessor.CreateStoragePseudoScalar(Signature.VSpaceDimension);

            PseudoScalar = OmOrthonormalToTarget.MapKVector(sourcePseudoScalar);
            PseudoScalarInverse = ScalarProcessor.BladeInverse(Signature, PseudoScalar);
            PseudoScalarReverse = ScalarProcessor.Reverse(PseudoScalar).GetKVectorPart(PseudoScalar.Grade);
        }



        public override T Sp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            return ScalarProcessor.Sp(Signature, s1, s2);
        }

        public override IGaStorageMultivector<T> Lcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Lcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaStorageMultivector<T> Rcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Rcp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaStorageMultivector<T> Hip(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Hip(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaStorageMultivector<T> Fdp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Fdp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaStorageMultivector<T> Acp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Acp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGaStorageMultivector<T> Cp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = ScalarProcessor.Cp(Signature, s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override T Sp(IGaStorageMultivector<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.Sp(Signature, s1);
        }

        public override T NormSquared(IGaStorageMultivector<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return ScalarProcessor.NormSquared(Signature, s1);
        }
    }
}