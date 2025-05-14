using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Reflectors;

public interface IXGaReflector<T> : 
    IXGaVersor<T>
{
    IXGaReflector<T> GetReflectorInverse();
}