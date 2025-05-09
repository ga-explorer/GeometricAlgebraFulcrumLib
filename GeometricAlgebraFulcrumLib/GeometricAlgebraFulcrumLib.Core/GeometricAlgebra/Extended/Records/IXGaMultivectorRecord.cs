using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaFloat64Multivector Multivector { get; }
}
