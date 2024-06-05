namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public interface IRGaRotor<T> : 
    IRGaScaledRotor<T>
{
    IRGaRotor<T> GetRotorInverse();
}