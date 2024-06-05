using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

public interface IRGaFloat64Versor : 
    IRGaFloat64Automorphism
{
    IRGaFloat64Versor GetVersorInverse();

    RGaFloat64Multivector GetMultivector();

    RGaFloat64Multivector GetMultivectorReverse();

    RGaFloat64Multivector GetMultivectorInverse();
}