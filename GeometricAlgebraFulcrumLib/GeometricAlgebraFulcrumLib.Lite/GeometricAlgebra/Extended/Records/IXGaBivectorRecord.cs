using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public interface IXGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaFloat64Bivector Bivector { get; }
}
