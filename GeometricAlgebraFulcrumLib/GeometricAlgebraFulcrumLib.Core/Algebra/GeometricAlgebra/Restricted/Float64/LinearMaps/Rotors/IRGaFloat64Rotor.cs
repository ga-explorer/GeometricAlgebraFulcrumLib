namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public interface IRGaFloat64Rotor : 
    IRGaFloat64ScaledRotor
{
    IRGaFloat64Rotor GetRotorInverse();
}