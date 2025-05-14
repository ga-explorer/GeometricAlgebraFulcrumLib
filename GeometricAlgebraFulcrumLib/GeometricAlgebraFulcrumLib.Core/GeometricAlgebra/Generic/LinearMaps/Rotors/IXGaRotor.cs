namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Rotors;

public interface IXGaRotor<T> : 
    IXGaScaledRotor<T>
{
    IXGaRotor<T> GetRotorInverse();
}