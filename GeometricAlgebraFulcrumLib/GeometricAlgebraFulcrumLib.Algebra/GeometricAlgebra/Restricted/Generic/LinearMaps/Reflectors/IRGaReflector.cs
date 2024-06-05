using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors;

public interface IRGaReflector<T> : 
    IRGaVersor<T>
{
    IRGaReflector<T> GetReflectorInverse();
}