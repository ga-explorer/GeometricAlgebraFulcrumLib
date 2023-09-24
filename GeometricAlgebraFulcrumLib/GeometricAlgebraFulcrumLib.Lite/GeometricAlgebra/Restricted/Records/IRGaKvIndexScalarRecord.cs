using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records
{
    public interface IRGaKvIndexScalarRecord<out T> :
        IRGaKvIndexRecord,
        IGaScalarRecord<T>
    {
    }
}