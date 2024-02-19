using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexPairScalarRecord<out T> :
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}