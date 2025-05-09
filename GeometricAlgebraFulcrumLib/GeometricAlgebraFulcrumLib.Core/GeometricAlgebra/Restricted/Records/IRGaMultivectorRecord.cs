using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaFloat64Multivector Multivector { get; }
}
