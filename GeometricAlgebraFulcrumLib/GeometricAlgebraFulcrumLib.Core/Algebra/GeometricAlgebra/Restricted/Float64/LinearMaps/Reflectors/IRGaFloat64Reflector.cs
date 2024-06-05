using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public interface IRGaFloat64Reflector : 
    IRGaFloat64Versor
{
    IRGaFloat64Reflector GetReflectorInverse();
}