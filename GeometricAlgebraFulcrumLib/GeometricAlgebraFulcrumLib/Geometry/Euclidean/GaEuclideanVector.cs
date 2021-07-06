using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaEuclideanVector<T>
        : IGaEuclideanGeometry<T>
    {
        public static GaEuclideanVector<T> Create(IGaVectorStorage<T> storage)
        {
            return new(storage);
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaVectorStorage<T> Storage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaEuclideanVector([NotNull] IGaVectorStorage<T> storage)
        {
            Storage = storage;
        }


        public GaEuclideanVector<T> GetRotatedVector(IGaRotor<T> rotor)
        {
            return new(
                rotor.MapVector(Storage).GetVectorPart()
            );
        }

        public GaEuclideanVector<T> GetProjectedVector(GaEuclideanSubspace<T> subspace)
        {
            return new(
                subspace.Project(Storage).GetVectorPart()
            );
        }
    }
}