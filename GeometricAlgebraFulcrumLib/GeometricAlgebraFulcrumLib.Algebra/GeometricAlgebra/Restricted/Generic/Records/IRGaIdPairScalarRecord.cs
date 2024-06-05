using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}