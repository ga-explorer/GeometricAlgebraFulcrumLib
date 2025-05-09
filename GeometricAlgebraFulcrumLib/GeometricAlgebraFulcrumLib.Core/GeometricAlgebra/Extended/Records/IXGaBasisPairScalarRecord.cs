using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaBasisPairScalarRecord<out T> :
    IXGaBasisPairRecord,
    IGaScalarRecord<T>
{
}