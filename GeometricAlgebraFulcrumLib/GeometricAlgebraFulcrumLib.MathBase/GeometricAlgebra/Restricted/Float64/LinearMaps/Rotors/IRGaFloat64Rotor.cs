namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public interface IRGaFloat64Rotor : 
        IRGaFloat64ScaledRotor
    {
        IRGaFloat64Rotor GetRotorInverse();
    }
}