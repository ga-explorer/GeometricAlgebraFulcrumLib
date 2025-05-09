using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaFloat64KVector KVector { get; }
}
