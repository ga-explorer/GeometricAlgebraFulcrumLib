using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public interface IVersor<T> : 
        IAutomorphism<T>
    {
        IVersor<T> GetVersorInverse();

        IMultivectorStorage<T> GetMultivectorStorage();

        IMultivectorStorage<T> GetMultivectorInverseStorage();
    }
}