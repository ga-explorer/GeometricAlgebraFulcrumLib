using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaIdScalarRecord<out T> :
    IRGaIdRecord,
    IGaScalarRecord<T>
{
}