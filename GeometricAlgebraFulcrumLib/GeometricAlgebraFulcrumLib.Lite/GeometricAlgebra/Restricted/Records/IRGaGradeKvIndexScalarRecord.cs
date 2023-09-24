using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records
{
    public interface IRGaGradeKvIndexScalarRecord<out T> :
        IGaGradeRecord,
        IRGaKvIndexRecord,
        IGaScalarRecord<T>
    {
    }
}