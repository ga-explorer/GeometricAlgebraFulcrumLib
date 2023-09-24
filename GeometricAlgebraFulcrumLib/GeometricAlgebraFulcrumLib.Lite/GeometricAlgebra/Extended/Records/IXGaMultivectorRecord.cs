using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public interface IXGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaFloat64Multivector Multivector { get; }
}
