using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaFloat64Multivector Multivector { get; }
}
