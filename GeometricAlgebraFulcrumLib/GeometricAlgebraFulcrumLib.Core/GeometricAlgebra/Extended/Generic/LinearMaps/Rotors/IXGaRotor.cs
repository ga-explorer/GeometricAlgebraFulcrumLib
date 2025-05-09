namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public interface IXGaRotor<T> : 
    IXGaScaledRotor<T>
{
    IXGaRotor<T> GetRotorInverse();
}