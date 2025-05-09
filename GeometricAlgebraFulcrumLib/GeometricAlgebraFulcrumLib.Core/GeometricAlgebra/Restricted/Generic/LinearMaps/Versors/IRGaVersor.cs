using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

public interface IRGaVersor<T> : 
    IRGaAutomorphism<T>
{
    IRGaVersor<T> GetVersorInverse();

    RGaMultivector<T> GetMultivector();

    RGaMultivector<T> GetMultivectorReverse();

    RGaMultivector<T> GetMultivectorInverse();
}