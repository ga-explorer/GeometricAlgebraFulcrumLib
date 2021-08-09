using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGaSubspace<T> : 
        IGaGeometry<T>
    {
        uint SubspaceDimension { get; }

        IGaStorageKVector<T> Blade { get; }

        IGaStorageKVector<T> BladeInverse { get; }

        T BladeScalarProductSquared { get; }

        IGaStorageMultivector<T> Project(IGaStorageMultivector<T> mv);

        IGaStorageMultivector<T> Reflect(IGaStorageMultivector<T> mv);

        IGaStorageMultivector<T> Rotate(IGaStorageMultivector<T> mv);

        IGaStorageMultivector<T> VersorProduct(IGaStorageMultivector<T> mv);

        IGaStorageMultivector<T> Complement(IGaStorageMultivector<T> mv);
    }
}