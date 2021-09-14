using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public interface IRotor<T> : 
        IVersor<T>
    {
        IRotor<T> GetRotorInverse();
    }
}