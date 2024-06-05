using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

public interface IXGaFloat64ScaledRotor : 
    IXGaFloat64Versor
{
    double GetScalingFactor();

    IXGaFloat64ScaledRotor GetScaledRotorInverse();
}