using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexRecord,
    IGaScalarRecord<T>
{
}