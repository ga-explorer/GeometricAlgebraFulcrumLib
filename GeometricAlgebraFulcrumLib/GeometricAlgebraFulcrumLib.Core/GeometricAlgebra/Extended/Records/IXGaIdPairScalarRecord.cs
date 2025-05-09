using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaIdPairScalarRecord<out T> :
    IXGaIdPairRecord,
    IGaScalarRecord<T>
{
}