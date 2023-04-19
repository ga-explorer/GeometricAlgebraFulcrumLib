using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public interface IRGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaFloat64KVector KVector { get; }
}

public interface IRGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaKVector<T> KVector { get; }
}