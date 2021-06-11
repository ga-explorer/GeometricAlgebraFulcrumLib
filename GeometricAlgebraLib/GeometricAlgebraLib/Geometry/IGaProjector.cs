using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaProjector<T> : IGaVectorsLinearMap<T>
    {
        IGaKVectorStorage<T> UnitBladeStorage { get; }
    }
}