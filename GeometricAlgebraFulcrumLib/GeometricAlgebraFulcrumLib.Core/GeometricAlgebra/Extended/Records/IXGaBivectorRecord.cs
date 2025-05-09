using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaFloat64Bivector Bivector { get; }
}
