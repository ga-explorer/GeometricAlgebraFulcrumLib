using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

public interface IRGaVersor<T> : 
    IRGaAutomorphism<T>
{
    IRGaVersor<T> GetVersorInverse();

    RGaMultivector<T> GetMultivector();

    RGaMultivector<T> GetMultivectorReverse();

    RGaMultivector<T> GetMultivectorInverse();
}