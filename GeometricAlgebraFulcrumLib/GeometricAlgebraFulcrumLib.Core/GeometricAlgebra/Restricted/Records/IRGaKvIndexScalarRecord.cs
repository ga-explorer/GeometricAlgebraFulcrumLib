using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexScalarRecord<out T> :
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}