using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisPairScalarRecord<out T> :
    IRGaBasisPairRecord,
    IGaScalarRecord<T>
{
}