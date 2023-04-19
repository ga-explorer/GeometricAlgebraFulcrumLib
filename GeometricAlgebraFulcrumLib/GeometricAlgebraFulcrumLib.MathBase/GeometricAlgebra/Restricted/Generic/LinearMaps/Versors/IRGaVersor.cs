using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors
{
    public interface IRGaVersor<T> : 
        IRGaAutomorphism<T>
    {
        IRGaVersor<T> GetVersorInverse();

        RGaMultivector<T> GetMultivector();

        RGaMultivector<T> GetMultivectorReverse();

        RGaMultivector<T> GetMultivectorInverse();
    }
}