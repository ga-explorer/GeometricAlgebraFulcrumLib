using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaBasisScalarRecord<out T> :
    IXGaBasisRecord,
    IGaScalarRecord<T>
{
}