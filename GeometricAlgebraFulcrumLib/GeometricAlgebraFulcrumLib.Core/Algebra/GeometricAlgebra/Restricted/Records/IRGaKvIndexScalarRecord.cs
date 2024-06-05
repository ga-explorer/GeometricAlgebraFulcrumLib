using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaKvIndexScalarRecord<out T> :
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}