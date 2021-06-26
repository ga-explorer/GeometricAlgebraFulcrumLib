using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaSubspace<T> : IGaGeometricElement<T>
    {
        IGaKVectorStorage<T> BladeStorage { get; }

        T BladeNormSquared { get; }


    }
}