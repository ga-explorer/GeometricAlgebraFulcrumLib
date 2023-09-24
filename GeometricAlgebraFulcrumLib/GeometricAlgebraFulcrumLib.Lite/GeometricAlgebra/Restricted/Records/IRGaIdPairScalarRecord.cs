using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}