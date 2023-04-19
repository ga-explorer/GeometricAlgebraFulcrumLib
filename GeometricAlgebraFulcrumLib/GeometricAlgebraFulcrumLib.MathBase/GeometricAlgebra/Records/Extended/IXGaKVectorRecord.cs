using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public interface IXGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaFloat64KVector KVector { get; }
}

public interface IXGaKVectorRecord<T>
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaKVector<T> KVector { get; }
}