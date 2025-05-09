using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    RGaVector<T> Vector { get; }
}
