using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;

public interface IXGaFloat64Versor : 
    IXGaFloat64Automorphism
{
    IXGaFloat64Versor GetVersorInverse();

    XGaFloat64Multivector GetMultivector();

    XGaFloat64Multivector GetMultivectorReverse();

    XGaFloat64Multivector GetMultivectorInverse();
}