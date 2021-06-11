using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaReflector<T> : IGaVectorsLinearMap<T>
    {
        IGaKVectorStorage<T> BladeStorage { get; }

        T BladeNormSquared { get; }
    }
}