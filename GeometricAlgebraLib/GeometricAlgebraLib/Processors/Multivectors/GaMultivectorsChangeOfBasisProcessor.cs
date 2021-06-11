using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Outermorphisms;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processors.Multivectors
{
    public sealed class GaMultivectorsChangeOfBasisProcessor<T>
        : GaMultivectorsProcessor<T>
    {
        public GaMultivectorsProcessor<T> BaseProcessor { get; }

        public GaOmChangeOfBasis<T> Outermorphism { get; }


        public GaMultivectorsChangeOfBasisProcessor([NotNull] GaMultivectorsProcessor<T> baseProcessor, [NotNull] GaOmChangeOfBasis<T> outermorphism) 
            : base(baseProcessor.ScalarProcessor)
        {
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

        public override T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);
            var s2 = Outermorphism.ForwardOutermorphism.MapMultivector(storage2);

            var s = BaseProcessor.Sp(s1, s2);

            return s;
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

        public override IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1)
        {
            var s1 = Outermorphism.ForwardOutermorphism.MapMultivector(storage1);

            var s = BaseProcessor.GpSquared(s1);

            return Outermorphism.BackwardOutermorphism.MapMultivector(s);
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }

        public override T SpSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }

        public override T SpReverse(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }

        public override T NormSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
        }
    }
}