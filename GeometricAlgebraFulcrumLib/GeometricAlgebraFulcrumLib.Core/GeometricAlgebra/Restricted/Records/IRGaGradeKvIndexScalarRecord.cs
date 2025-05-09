using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}