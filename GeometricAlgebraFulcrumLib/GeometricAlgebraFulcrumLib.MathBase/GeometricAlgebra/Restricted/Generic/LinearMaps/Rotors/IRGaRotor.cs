namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors
{
    public interface IRGaRotor<T> : 
        IRGaScaledRotor<T>
    {
        IRGaRotor<T> GetRotorInverse();
    }
}