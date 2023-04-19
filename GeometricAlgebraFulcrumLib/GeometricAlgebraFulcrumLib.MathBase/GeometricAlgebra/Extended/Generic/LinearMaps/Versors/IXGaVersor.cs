using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors
{
    public interface IXGaVersor<T> : 
        IXGaAutomorphism<T>
    {
        IXGaVersor<T> GetVersorInverse();

        XGaMultivector<T> GetMultivector();

        XGaMultivector<T> GetMultivectorReverse();

        XGaMultivector<T> GetMultivectorInverse();
    }
}