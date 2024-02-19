using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public interface IRGaFloat64Reflector : 
    IRGaFloat64Versor
{
    IRGaFloat64Reflector GetReflectorInverse();
}