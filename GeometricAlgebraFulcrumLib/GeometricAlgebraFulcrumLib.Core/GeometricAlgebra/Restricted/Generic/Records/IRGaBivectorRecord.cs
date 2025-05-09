using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaBivector<T> Bivector { get; }
}