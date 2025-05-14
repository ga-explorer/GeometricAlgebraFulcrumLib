using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public interface IXGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaBivector<T> Bivector { get; }
}