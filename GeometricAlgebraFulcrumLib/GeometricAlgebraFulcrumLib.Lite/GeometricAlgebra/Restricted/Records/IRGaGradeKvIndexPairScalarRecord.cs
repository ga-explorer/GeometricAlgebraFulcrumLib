using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaGradeKvIndexPairScalarRecord<out T> :
    IGaGradeRecord,
    IRGaKvIndexPairRecord,
    IGaScalarRecord<T>
{
}