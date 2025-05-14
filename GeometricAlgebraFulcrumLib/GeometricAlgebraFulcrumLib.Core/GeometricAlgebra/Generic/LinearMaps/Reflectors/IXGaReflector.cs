using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Reflectors;

public interface IXGaReflector<T> : 
    IXGaVersor<T>
{
    IXGaReflector<T> GetReflectorInverse();
}