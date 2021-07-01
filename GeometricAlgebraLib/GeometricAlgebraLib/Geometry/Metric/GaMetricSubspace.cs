using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public sealed class GaMetricSubspace<T> 
        : IGaMetricGeometry<T>, IGaSubspace<T>
    {
        public static GaMetricSubspace<T> Create(IGaMultivectorsProcessor<T> processor, IGaKVectorStorage<T> storage)
        {
            return new GaMetricSubspace<T>(processor, storage);
        }

        public static GaMetricSubspace<T> CreateFromPseudoScalar(IGaMultivectorsProcessor<T> processor, IGaScalarProcessor<T> scalarProcessor, int vSpaceDimension)
        {
            return new GaMetricSubspace<T>(
                processor,
                GaKVectorTermStorage<T>.CreatePseudoScalar(
                    processor.ScalarProcessor, 
                    vSpaceDimension
                )
            );
        }


        public IGaMultivectorsProcessor<T> MultivectorProcessor { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid 
            => false;

        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorProcessor.ScalarProcessor;

        public IGaKVectorStorage<T> BladeStorage { get; }

        public T BladeNormSquared { get; }


        private GaMetricSubspace([NotNull] IGaMultivectorsProcessor<T> processor, [NotNull] IGaKVectorStorage<T> storage)
        {
            BladeStorage = storage;
            BladeNormSquared = processor.NormSquared(storage);
            MultivectorProcessor = processor;
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return MultivectorProcessor.Lcp(
                MultivectorProcessor.Lcp(storage, BladeStorage),
                BladeStorage
            ).Divide(BladeNormSquared);
        }

        public IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> Complement(IGaKVectorStorage<T> vectorStorage)
        {
            throw new System.NotImplementedException();
        }
    }
}