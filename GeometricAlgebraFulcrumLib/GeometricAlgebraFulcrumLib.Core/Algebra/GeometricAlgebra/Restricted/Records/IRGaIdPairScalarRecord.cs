using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}