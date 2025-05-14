using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public interface IXGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaKVector<T> KVector { get; }
}