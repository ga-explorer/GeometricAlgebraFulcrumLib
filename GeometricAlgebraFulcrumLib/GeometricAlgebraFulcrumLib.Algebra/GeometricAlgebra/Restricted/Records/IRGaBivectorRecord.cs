using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaFloat64Bivector Bivector { get; }
}
