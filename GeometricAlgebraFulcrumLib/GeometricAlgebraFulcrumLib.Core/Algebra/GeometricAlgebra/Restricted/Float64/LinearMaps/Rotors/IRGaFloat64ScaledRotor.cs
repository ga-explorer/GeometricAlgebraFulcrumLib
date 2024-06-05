using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public interface IRGaFloat64ScaledRotor : 
    IRGaFloat64Versor
{
    double GetScalingFactor();

    IRGaFloat64ScaledRotor GetScaledRotorInverse();
}