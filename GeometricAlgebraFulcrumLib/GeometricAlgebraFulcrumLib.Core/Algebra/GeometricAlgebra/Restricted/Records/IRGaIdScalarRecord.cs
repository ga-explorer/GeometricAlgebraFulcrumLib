using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaIdScalarRecord<out T> :
    IRGaIdRecord,
    IGaScalarRecord<T>
{
}