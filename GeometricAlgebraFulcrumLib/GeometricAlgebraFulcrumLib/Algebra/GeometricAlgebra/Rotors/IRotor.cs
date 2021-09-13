using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public interface IRotor<T> : 
        IAutomorphism<T>
    {
        IRotor<T> GetReverse();

        IMultivectorStorage<T> GetMultivectorStorage();

        IMultivectorStorage<T> GetMultivectorReverseStorage();
    }
}