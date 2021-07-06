using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public sealed class GaMultivectorsProcessorChangeOfBasis<T>
        : GaMultivectorsProcessorBase<T>
    {
        public override IGaSignature Signature { get; }

        public override IGaKVectorStorage<T> PseudoScalar { get; }
        
        public override IGaKVectorStorage<T> PseudoScalarInverse { get; }
        
        public override IGaKVectorStorage<T> PseudoScalarReverse { get; }

        public GaMultivectorsProcessorBase<T> BaseProcessor { get; }

        public GaOmChangeOfBasis<T> Outermorphism { get; }


        public GaMultivectorsProcessorChangeOfBasis([NotNull] GaMultivectorsProcessorBase<T> baseProcessor, [NotNull] GaOmChangeOfBasis<T> outermorphism) 
            : base(baseProcessor.ScalarProcessor)
        {
            Signature = baseProcessor.Signature;
            BaseProcessor = baseProcessor;
            Outermorphism = outermorphism;
        }


        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Gp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.GpReverse(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            return BaseProcessor.Sp(s1, s2);
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Lcp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Rcp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Hip(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Fdp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Acp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Cp(s1, s2);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);

            var s = BaseProcessor.Gp(s1);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);

            var s = BaseProcessor.GpReverse(s1);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override T Sp(IGaMultivectorStorage<T> storage1)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);

            return BaseProcessor.Sp(s1);
        }

        public override T NormSquared(IGaMultivectorStorage<T> storage1)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);

            return BaseProcessor.NormSquared(s1);
        }
    }
}