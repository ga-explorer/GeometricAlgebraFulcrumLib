using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;

public interface IXGaReflector<T> : 
    IXGaVersor<T>
{
    IXGaReflector<T> GetReflectorInverse();
}