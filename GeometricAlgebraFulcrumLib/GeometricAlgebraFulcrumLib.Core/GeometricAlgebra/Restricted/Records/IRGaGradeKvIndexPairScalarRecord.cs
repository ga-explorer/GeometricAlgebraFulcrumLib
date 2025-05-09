using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexPairScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}