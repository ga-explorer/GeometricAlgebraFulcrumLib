using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

public interface IXGaFloat64Versor : 
    IXGaFloat64Automorphism
{
    IXGaFloat64Versor GetVersorInverse();

    XGaFloat64Multivector GetMultivector();

    XGaFloat64Multivector GetMultivectorReverse();

    XGaFloat64Multivector GetMultivectorInverse();
}