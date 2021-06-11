using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processors.Multivectors
{
    public abstract class GaMultivectorsProcessor<T>
    {
        public static GaMultivectorsEuclideanProcessor<T> CreateEuclidean(IGaScalarProcessor<T> scalarProcessor)
        {
            return new(scalarProcessor);
        }


        public IGaScalarProcessor<T> ScalarProcessor { get; }


        protected GaMultivectorsProcessor([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }

        
        public abstract IGaMultivectorStorage<T> Gp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public IGaMultivectorStorage<T> Gp(params IGaMultivectorStorage<T>[] storagesList)
        {
            return storagesList.Skip(1).Aggregate(
                storagesList[0], 
                Gp
            );
        }

        public abstract T Sp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2);

        public abstract IGaMultivectorStorage<T> GpSquared(IGaMultivectorStorage<T> storage1);

        public abstract IGaMultivectorStorage<T> GpReverse(IGaMultivectorStorage<T> storage1);

        public abstract T SpSquared(IGaMultivectorStorage<T> storage1);

        public abstract T SpReverse(IGaMultivectorStorage<T> storage1);

        public abstract T NormSquared(IGaMultivectorStorage<T> storage1);

        public T Norm(IGaMultivectorStorage<T> storage1)
        {
            return ScalarProcessor.Sqrt(NormSquared(storage1));
        }

        public abstract IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> storage1);

        public abstract IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> storage1);
    }
}