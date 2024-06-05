using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaKVector<T> KVector { get; }
}