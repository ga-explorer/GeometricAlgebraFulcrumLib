using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public interface IRGaScaledRotor<T> : 
    IRGaVersor<T>
{
    Scalar<T> GetScalingFactor();

    IRGaScaledRotor<T> GetScaledRotorInverse();
}