using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Generic
{
    public sealed class GaProcessorGenericChangeOfBasis<T>
        : GaProcessorGenericBase<T>, IGaProcessorChangeOfBasis<T>
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

        public override IGasKVector<T> PseudoScalar { get; }
        
        public override IGasKVector<T> PseudoScalarInverse { get; }
        
        public override IGasKVector<T> PseudoScalarReverse { get; }


        internal GaProcessorGenericChangeOfBasis([NotNull] IGaSignature signature, [NotNull] IGaOutermorphism<T> omTargetToOrthonormal, [NotNull] IGaOutermorphism<T> omOrthonormalToTarget) 
            : base(omTargetToOrthonormal.ScalarProcessor)
        {
            Signature = signature;
            OmTargetToOrthonormal = omTargetToOrthonormal;
            OmOrthonormalToTarget = omOrthonormalToTarget;

            var sourcePseudoScalar =
                ScalarProcessor.CreatePseudoScalar(Signature.VSpaceDimension);

            PseudoScalar = OmOrthonormalToTarget.MapKVector(sourcePseudoScalar);
            PseudoScalarInverse = Signature.BladeInverse(PseudoScalar);
            PseudoScalarReverse = PseudoScalar.GetReverse().GetKVectorPart(PseudoScalar.Grade);
        }



        public override T Sp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            return Signature.Sp(s1, s2);
        }

        public override IGasMultivector<T> Lcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Lcp(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGasMultivector<T> Rcp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Rcp(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGasMultivector<T> Hip(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Hip(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGasMultivector<T> Fdp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Fdp(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGasMultivector<T> Acp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Acp(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override IGasMultivector<T> Cp(IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = OmTargetToOrthonormal.MapMultivector(mv2);

            var s = Signature.Cp(s1, s2);

            return OmOrthonormalToTarget.MapMultivector(s);
        }

        public override T Sp(IGasMultivector<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return Signature.Sp(s1);
        }

        public override T NormSquared(IGasMultivector<T> mv1)
        {
            var s1 = OmTargetToOrthonormal.MapMultivector(mv1);

            return Signature.NormSquared(s1);
        }
    }
}