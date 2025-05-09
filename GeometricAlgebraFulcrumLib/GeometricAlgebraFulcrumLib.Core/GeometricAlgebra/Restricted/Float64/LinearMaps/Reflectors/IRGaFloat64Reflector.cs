using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public interface IRGaFloat64Reflector : 
    IRGaFloat64Versor
{
    IRGaFloat64Reflector GetReflectorInverse();
}