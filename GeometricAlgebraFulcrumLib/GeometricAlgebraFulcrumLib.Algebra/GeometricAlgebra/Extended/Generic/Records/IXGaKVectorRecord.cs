using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaKVector<T> KVector { get; }
}