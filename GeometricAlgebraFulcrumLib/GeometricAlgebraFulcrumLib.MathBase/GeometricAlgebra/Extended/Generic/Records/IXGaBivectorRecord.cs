using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaBivector<T> Bivector { get; }
}