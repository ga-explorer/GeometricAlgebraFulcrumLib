using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public sealed class GaMetricSubspace<T> 
        : IGaMetricGeometry<T>
    {
        public GaMultivectorsProcessor<T> Processor { get; }

        public bool IsValid 
            => true;

        public bool IsInvalid 
            => false;

        public IGaScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage { get; }

        public T BladeNormSquared { get; }


        internal GaMetricSubspace([NotNull] GaMultivectorsProcessor<T> processor, [NotNull] IGaMultivectorStorage<T> storage, [NotNull] T bladeNormSquared)
        {
            Storage = storage;
            BladeNormSquared = bladeNormSquared;
            Processor = processor;
        }


        public IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage)
        {
            return Processor.Lcp(
                Processor.Lcp(storage, Storage),
                Storage
            ).Divide(BladeNormSquared);
        }
    }
}