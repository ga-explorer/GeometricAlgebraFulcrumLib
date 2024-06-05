using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Reflectors;

public interface IXGaFloat64Reflector : 
    IXGaFloat64Versor
{
    IXGaFloat64Reflector GetReflectorInverse();
}