using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Geometry
{
    public interface IGaSubspace<T> : IGaGeometricElement<T>
    {
        IGaKVectorStorage<T> BladeStorage { get; }

        T BladeNormSquared { get; }

        IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage);

        IGaKVectorStorage<T> Complement(IGaKVectorStorage<T> vectorStorage);
    }
}