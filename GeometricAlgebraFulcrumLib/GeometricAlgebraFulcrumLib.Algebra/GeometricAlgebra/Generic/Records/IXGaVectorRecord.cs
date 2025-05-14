using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public interface IXGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    XGaVector<T> Vector { get; }
}
