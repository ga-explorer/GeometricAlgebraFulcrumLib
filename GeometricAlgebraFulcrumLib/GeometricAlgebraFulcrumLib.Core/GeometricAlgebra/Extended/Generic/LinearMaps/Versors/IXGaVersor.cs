using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

public interface IXGaVersor<T> : 
    IXGaAutomorphism<T>
{
    IXGaVersor<T> GetVersorInverse();

    XGaMultivector<T> GetMultivector();

    XGaMultivector<T> GetMultivectorReverse();

    XGaMultivector<T> GetMultivectorInverse();
}