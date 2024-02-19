using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

public interface IXGaFloat64Versor : 
    IXGaFloat64Automorphism
{
    IXGaFloat64Versor GetVersorInverse();

    XGaFloat64Multivector GetMultivector();

    XGaFloat64Multivector GetMultivectorReverse();

    XGaFloat64Multivector GetMultivectorInverse();
}