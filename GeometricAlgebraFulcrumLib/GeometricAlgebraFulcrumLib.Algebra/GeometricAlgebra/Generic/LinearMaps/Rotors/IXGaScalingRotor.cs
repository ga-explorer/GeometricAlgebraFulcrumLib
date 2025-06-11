using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

public interface IXGaScalingRotor<T> : 
    IXGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IXGaScalingRotor<T> GetScalingRotorInverse();
}