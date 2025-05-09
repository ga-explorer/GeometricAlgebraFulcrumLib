using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaFloat64Bivector Bivector { get; }
}
