using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisPairScalarRecord<out T> :
    IRGaBasisPairRecord,
    IGaScalarRecord<T>
{
}