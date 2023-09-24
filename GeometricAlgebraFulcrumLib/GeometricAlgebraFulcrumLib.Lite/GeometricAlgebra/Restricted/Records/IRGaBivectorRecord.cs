using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public interface IRGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaFloat64Bivector Bivector { get; }
}
