using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    RGaVector<T> Vector { get; }
}
