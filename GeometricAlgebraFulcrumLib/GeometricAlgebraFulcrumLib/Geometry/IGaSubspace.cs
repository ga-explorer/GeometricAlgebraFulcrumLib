using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaSubspace<T> : 
        IGaGeometry<T>
    {
        uint SubspaceDimension { get; }

        IGasKVector<T> Blade { get; }

        IGasKVector<T> BladeInverse { get; }

        T BladeScalarProductSquared { get; }

        IGasMultivector<T> Project(IGasMultivector<T> mv);

        IGasMultivector<T> Reflect(IGasMultivector<T> mv);

        IGasMultivector<T> Rotate(IGasMultivector<T> mv);

        IGasMultivector<T> VersorProduct(IGasMultivector<T> mv);

        IGasMultivector<T> Complement(IGasMultivector<T> mv);
    }
}