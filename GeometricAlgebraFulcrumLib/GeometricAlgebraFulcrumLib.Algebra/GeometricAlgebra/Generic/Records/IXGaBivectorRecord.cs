using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public interface IXGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    XGaBivector<T> Bivector { get; }
}