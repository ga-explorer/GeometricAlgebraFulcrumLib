using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public sealed class GaMetricVector<T>
        : IGaMetricGeometry<T>
    {
        public GaMultivectorsProcessor<T> Processor { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaVectorStorage<T> Storage { get; }


        internal GaMetricVector([NotNull] GaMultivectorsProcessor<T> processor, IGaVectorStorage<T> storage)
        {
            Storage = storage;

            Processor = processor;
        }


        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        public GaMetricVector<T> GetRotatedVector(GaEuclideanRotor<T> rotor)
        {
            return new(
                Processor,
                rotor.MapVector(Storage).GetVectorPart()
            );
        }

        public GaMetricVector<T> GetProjectedVector(GaMetricSubspace<T> subspace)
        {
            return new(
                Processor,
                subspace.Project(Storage).GetVectorPart()
            );
        }
    }
}