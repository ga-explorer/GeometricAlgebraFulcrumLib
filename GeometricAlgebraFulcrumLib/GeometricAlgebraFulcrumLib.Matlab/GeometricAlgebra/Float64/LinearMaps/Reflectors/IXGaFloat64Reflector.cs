using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Reflectors;

public interface IXGaFloat64Reflector : 
    IXGaFloat64Versor
{
    IXGaFloat64Reflector GetReflectorInverse();
}