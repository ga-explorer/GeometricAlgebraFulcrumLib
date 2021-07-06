using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Metric
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