using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    RGaVector<T> Vector { get; }
}
