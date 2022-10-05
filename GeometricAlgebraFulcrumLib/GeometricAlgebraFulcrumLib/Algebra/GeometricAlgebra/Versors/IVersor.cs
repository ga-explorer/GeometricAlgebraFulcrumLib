using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public interface IVersor<T> : 
        IAutomorphism<T>, 
        IMultivectorStorageContainer<T>
    {
        IVersor<T> GetVersorInverse();

        GaMultivector<T> GetMultivector();

        GaMultivector<T> GetMultivectorReverse();

        GaMultivector<T> GetMultivectorInverse();

        IMultivectorStorage<T> GetMultivectorStorageReverse();

        IMultivectorStorage<T> GetMultivectorStorageInverse();
    }
}