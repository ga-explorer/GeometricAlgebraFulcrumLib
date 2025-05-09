using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors;

public interface IRGaReflector<T> : 
    IRGaVersor<T>
{
    IRGaReflector<T> GetReflectorInverse();
}