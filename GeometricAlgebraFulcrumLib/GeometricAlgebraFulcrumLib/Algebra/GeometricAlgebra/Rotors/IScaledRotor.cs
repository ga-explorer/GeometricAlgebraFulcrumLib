using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public interface IScaledRotor<T> : 
        IVersor<T>
    {
        Scalar<T> GetScalingFactor();

        IScaledRotor<T> GetScaledRotorInverse();
    }
}