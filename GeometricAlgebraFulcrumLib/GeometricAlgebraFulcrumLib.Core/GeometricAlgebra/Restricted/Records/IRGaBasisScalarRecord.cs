using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisScalarRecord<out T> :
    IRGaBasisRecord,
    IGaScalarRecord<T>
{
}