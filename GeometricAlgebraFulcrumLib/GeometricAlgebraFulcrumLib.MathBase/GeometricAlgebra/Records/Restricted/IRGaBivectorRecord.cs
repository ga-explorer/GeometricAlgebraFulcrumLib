using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public interface IRGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaFloat64Bivector Bivector { get; }
}

public interface IRGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaBivector<T> Bivector { get; }
}