using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public interface IXGaScaledRotor<T> : 
        IXGaVersor<T>
    {
        Scalar<T> GetScalingFactor();

        IXGaScaledRotor<T> GetScaledRotorInverse();
    }
}