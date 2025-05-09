using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}