using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaFloat64KVector KVector { get; }
}
