using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    XGaVector<T> Vector { get; }
}
