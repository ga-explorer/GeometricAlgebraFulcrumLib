using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    XGaVector<T> Vector { get; }
}
