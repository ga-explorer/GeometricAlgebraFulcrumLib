using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public sealed class GaMetricVector<T>
        : IGaMetricGeometry<T>
    {
        public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaVectorStorage<T> Storage { get; }


        internal GaMetricVector([NotNull] IGaMultivectorProcessor<T> processor, IGaVectorStorage<T> storage)
        {
            Storage = storage;

            MultivectorProcessor = processor;
        }


        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        public GaMetricVector<T> GetRotatedVector(GaEuclideanRotor<T> rotor)
        {
            return new GaMetricVector<T>(
                MultivectorProcessor,
                rotor.MapVector(Storage).GetVectorPart()
            );
        }

        public GaMetricVector<T> GetProjectedVector(GaMetricSubspace<T> subspace)
        {
            return new GaMetricVector<T>(
                MultivectorProcessor,
                subspace.Project(Storage).GetVectorPart()
            );
        }
    }
}