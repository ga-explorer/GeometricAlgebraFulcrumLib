using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaKVector<T> KVector { get; }
}