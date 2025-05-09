using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    XGaVector<T> Vector { get; }
}
