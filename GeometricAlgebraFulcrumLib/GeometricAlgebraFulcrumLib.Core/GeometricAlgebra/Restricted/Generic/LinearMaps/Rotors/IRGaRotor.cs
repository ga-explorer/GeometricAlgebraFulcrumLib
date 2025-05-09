namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public interface IRGaRotor<T> : 
    IRGaScaledRotor<T>
{
    IRGaRotor<T> GetRotorInverse();
}