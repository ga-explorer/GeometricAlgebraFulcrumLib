using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaFloat64Bivector Bivector { get; }
}
