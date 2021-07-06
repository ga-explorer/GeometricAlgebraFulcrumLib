using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaProjector<T> : IGaVectorsLinearMap<T>
    {
        IGaKVectorStorage<T> UnitBladeStorage { get; }
    }
}