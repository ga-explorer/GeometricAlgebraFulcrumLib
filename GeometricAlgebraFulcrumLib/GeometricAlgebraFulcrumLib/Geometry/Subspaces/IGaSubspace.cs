using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGeoSubspace<T> : 
        IGeometricAlgebraElement<T>
    {
        uint SubspaceDimension { get; }

        KVectorStorage<T> Blade { get; }

        KVectorStorage<T> BladeInverse { get; }

        T BladeScalarProductSquared { get; }

        IMultivectorStorage<T> Project(IMultivectorStorage<T> mv);

        IMultivectorStorage<T> Reflect(IMultivectorStorage<T> mv);

        IMultivectorStorage<T> Rotate(IMultivectorStorage<T> mv);

        IMultivectorStorage<T> VersorProduct(IMultivectorStorage<T> mv);

        IMultivectorStorage<T> Complement(IMultivectorStorage<T> mv);
    }
}