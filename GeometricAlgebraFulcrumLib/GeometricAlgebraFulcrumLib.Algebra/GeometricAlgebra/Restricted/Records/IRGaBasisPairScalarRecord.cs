using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisPairScalarRecord<out T> :
    IRGaBasisPairRecord,
    IGaScalarRecord<T>
{
}