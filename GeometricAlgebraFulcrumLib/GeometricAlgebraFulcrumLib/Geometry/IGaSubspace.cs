using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaSubspace<T> : IGaGeometricElement<T>
    {
        IGaKVectorStorage<T> BladeStorage { get; }

        T BladeScalarProductSquared { get; }

        IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> storage);

        IGaMultivectorStorage<T> Complement(IGaMultivectorStorage<T> vectorStorage);
    }
}