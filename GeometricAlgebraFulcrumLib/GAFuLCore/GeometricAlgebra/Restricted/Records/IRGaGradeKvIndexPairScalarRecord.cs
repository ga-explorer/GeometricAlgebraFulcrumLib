using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexPairScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}