using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaBivector<T> Bivector { get; }
}