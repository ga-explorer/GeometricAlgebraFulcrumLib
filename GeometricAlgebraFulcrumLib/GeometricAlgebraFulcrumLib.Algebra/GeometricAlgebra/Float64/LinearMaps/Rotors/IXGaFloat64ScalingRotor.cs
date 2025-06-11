using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public interface IXGaFloat64ScalingRotor : 
    IXGaFloat64Versor
{
    double GetScalingFactor();

    IXGaFloat64ScalingRotor GetScalingRotorInverse();
}