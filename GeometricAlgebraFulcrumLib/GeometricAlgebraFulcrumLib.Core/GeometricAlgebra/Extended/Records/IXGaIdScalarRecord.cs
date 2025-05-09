using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaIdScalarRecord<out T> :
    IXGaIdRecord,
    IGaScalarRecord<T>
{
}