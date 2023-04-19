using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public interface IXGaBivectorRecord
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaFloat64Bivector Bivector { get; }
}

public interface IXGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaBivector<T> Bivector { get; }
}