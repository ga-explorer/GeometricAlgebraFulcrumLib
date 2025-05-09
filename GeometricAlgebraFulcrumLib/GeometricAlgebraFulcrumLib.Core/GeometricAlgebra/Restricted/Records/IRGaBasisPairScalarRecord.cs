using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisPairScalarRecord<out T> :
    IRGaBasisPairRecord,
    IGaScalarRecord<T>
{
}