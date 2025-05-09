using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaFloat64KVector KVector { get; }
}
