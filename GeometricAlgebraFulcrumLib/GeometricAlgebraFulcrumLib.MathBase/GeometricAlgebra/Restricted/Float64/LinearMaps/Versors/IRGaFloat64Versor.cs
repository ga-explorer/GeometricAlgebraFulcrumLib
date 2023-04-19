using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors
{
    public interface IRGaFloat64Versor : 
        IRGaFloat64Automorphism
    {
        IRGaFloat64Versor GetVersorInverse();

        RGaFloat64Multivector GetMultivector();

        RGaFloat64Multivector GetMultivectorReverse();

        RGaFloat64Multivector GetMultivectorInverse();
    }
}