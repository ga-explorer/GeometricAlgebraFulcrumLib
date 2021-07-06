using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaRotor<T> : IGaVectorsLinearMap<T>
    {
        IGaMultivectorStorage<T> Storage { get; }

        IGaMultivectorStorage<T> StorageReverse { get; }
    }
}