using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaBasisScalarRecord<out T> :
    IXGaBasisRecord,
    IGaScalarRecord<T>
{
}