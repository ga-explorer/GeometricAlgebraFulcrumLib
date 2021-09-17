using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public interface IVersor<T> : 
        IAutomorphism<T>, 
        IMultivectorStorageContainer<T>
    {
        IVersor<T> GetVersorInverse();

        IMultivectorStorage<T> GetMultivectorInverseStorage();
    }
}