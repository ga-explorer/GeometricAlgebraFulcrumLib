using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaFloat64Multivector Multivector { get; }
}
