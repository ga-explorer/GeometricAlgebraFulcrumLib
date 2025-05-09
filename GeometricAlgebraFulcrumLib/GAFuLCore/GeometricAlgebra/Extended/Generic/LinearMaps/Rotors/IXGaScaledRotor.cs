using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public interface IXGaScaledRotor<T> : 
    IXGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IXGaScaledRotor<T> GetScaledRotorInverse();
}