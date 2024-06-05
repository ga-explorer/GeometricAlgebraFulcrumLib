using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaFloat64Bivector Bivector { get; }
}
