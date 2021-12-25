namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public interface IRotor<T> : 
        IScaledRotor<T>
    {
        IRotor<T> GetRotorInverse();
    }
}