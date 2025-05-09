using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaFloat64Multivector Multivector { get; }
}
