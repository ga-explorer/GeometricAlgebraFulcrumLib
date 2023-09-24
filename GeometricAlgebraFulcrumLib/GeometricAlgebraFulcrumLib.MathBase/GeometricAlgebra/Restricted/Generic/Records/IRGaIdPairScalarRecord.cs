using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaIdPairScalarRecord<out T> :
    IRGaIdPairRecord,
    IGaScalarRecord<T>
{
}