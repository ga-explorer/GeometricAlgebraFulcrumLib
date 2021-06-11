using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processors.Multivectors
{
    public sealed class GaMultivectorsOrthonormalProcessor<T>
        : GaMultivectorsProcessor<T>
    {
        public GaOrthonormalBasesSignature BasesSignature { get; }


        public GaMultivectorsOrthonormalProcessor(IGaScalarProcessor<T> scalarProcessor, [NotNull] GaOrthonormalBasesSignature basesSignature) 
            : base(scalarProcessor)
        {
            BasesSignature = basesSignature;
        }


        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            throw new System.NotImplementedException();
        }

        public override IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1)
        {
            throw new System.NotImplementedException();
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