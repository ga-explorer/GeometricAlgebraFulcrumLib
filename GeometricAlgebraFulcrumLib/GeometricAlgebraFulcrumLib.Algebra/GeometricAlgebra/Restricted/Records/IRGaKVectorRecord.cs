using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    RGaFloat64KVector KVector { get; }
}
