using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public interface IXGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    XGaVector<T> Vector { get; }
}
