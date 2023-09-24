using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaIdScalarRecord<out T> :
    IRGaIdRecord,
    IGaScalarRecord<T>
{
}