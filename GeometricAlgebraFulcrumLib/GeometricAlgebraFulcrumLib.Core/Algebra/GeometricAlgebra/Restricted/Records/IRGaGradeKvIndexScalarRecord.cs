using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}