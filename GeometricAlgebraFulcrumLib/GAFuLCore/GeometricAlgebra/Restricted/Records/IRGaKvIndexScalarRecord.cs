using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexScalarRecord<out T> :
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}