using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaFloat64KVector KVector { get; }
}
