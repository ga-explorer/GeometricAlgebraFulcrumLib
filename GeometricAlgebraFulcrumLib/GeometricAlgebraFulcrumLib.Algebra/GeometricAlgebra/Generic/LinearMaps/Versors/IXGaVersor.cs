using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;

public interface IXGaVersor<T> : 
    IXGaAutomorphism<T>
{
    IXGaVersor<T> GetVersorInverse();

    XGaMultivector<T> GetMultivector();

    XGaMultivector<T> GetMultivectorReverse();

    XGaMultivector<T> GetMultivectorInverse();
}