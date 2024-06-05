using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaBivector<T> Bivector { get; }
}