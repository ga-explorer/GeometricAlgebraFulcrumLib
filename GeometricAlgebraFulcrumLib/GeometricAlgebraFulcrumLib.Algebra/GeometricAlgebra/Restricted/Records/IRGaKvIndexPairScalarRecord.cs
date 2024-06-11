using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexPairScalarRecord<out T> :
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}