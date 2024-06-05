using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public interface IRGaScaledRotor<T> : 
    IRGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IRGaScaledRotor<T> GetScaledRotorInverse();
}