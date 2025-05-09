using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisScalarRecord<out T> :
    IRGaBasisRecord,
    IGaScalarRecord<T>
{
}