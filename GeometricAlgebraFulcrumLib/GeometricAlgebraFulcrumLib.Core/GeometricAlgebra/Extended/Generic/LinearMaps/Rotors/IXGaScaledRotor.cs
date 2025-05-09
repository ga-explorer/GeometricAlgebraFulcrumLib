using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public interface IXGaScaledRotor<T> : 
    IXGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IXGaScaledRotor<T> GetScaledRotorInverse();
}