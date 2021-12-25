using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public interface IScaledRotor<T> : 
        IVersor<T>
    {
        T GetScalingFactor();

        IScaledRotor<T> GetScaledRotorInverse();
    }
}