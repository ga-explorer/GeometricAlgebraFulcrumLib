using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}