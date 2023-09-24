using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisScalarRecord<out T> :
    IRGaBasisRecord,
    IGaScalarRecord<T>
{
}