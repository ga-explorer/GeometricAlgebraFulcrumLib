namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public interface IXGaFloat64Rotor : 
    IXGaFloat64ScaledRotor
{
    IXGaFloat64Rotor GetRotorInverse();
}