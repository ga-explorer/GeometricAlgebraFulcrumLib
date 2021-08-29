using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGaSubspace<T> : 
        IGaGeometry<T>
    {
        uint SubspaceDimension { get; }

        IGaKVectorStorage<T> Blade { get; }

        IGaKVectorStorage<T> BladeInverse { get; }

        T BladeScalarProductSquared { get; }

        IGaMultivectorStorage<T> Project(IGaMultivectorStorage<T> mv);

        IGaMultivectorStorage<T> Reflect(IGaMultivectorStorage<T> mv);

        IGaMultivectorStorage<T> Rotate(IGaMultivectorStorage<T> mv);

        IGaMultivectorStorage<T> VersorProduct(IGaMultivectorStorage<T> mv);

        IGaMultivectorStorage<T> Complement(IGaMultivectorStorage<T> mv);
    }
}