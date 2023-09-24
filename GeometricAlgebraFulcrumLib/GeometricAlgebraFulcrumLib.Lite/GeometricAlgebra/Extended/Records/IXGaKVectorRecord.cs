using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public interface IXGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaFloat64KVector KVector { get; }
}
