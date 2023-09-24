using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaFloat64KVector KVector { get; }
}
