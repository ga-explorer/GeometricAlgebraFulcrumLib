using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors
{
    public interface IReflector<T> : 
        IVersor<T>
    {
        IReflector<T> GetReflectorInverse();
    }
}