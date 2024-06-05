using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;

public interface IXGaReflector<T> : 
    IXGaVersor<T>
{
    IXGaReflector<T> GetReflectorInverse();
}