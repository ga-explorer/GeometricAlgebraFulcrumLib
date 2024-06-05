using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexPairScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}