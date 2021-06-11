using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processors.Multivectors
{
    public sealed class GaMultivectorsEuclideanProcessor<T>
        : GaMultivectorsProcessor<T>
    {
        internal GaMultivectorsEuclideanProcessor(IGaScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor)
        {
        }


        public override IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.EGp(storage2);
        }

        public override T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.ESp(storage2);
        }

        public override IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.ELcp(storage2);
        }

        public override IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.ERcp(storage2);
        }

        public override IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.EHip(storage2);
        }

        public override IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.EFdp(storage2);
        }

        public override IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.EAcp(storage2);
        }

        public override IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            return storage1.ECp(storage2);
        }

        public override IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1)
        {
            return storage1.EGpSquared();
        }

        public override IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1)
        {
            return storage1.EGpReverse();
        }

        public override T SpSquared(IGaMultivectorStorage<T> storage1)
        {
            return storage1.ESpSquared();
        }

        public override T SpReverse(IGaMultivectorStorage<T> storage1)
        {
            return storage1.ESpReverse();
        }

        public override T NormSquared(IGaMultivectorStorage<T> storage1)
        {
            return storage1.ENormSquared();
        }

        public override IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1)
        {
            return storage1.EVersorInverse();
        }

        public override IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1)
        {
            return storage1.EBladeInverse();
        }
    }
}