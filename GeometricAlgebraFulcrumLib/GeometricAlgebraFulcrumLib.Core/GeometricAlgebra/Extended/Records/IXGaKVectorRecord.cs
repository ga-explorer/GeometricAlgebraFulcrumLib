using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaKVectorRecord
{
    /// <summary>
    /// The k-Vector
    /// </summary>
    XGaFloat64KVector KVector { get; }
}
