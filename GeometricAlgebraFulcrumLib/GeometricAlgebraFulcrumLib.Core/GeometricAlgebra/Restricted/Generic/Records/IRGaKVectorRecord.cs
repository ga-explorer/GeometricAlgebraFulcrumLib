using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaKVector<T> KVector { get; }
}