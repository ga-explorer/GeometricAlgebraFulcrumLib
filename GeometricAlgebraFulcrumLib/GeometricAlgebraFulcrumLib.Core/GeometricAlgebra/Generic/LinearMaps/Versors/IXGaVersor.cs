using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Versors;

public interface IXGaVersor<T> : 
    IXGaAutomorphism<T>
{
    IXGaVersor<T> GetVersorInverse();

    XGaMultivector<T> GetMultivector();

    XGaMultivector<T> GetMultivectorReverse();

    XGaMultivector<T> GetMultivectorInverse();
}