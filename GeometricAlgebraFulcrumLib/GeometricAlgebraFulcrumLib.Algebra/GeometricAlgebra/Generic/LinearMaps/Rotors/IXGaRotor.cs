namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

public interface IXGaRotor<T> : 
    IXGaScalingRotor<T>
{
    IXGaRotor<T> GetRotorInverse();
}