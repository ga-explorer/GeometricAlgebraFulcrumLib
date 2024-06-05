using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisScalarRecord<out T> :
    IRGaBasisRecord,
    IGaScalarRecord<T>
{
}