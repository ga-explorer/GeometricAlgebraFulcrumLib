using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaBivectorRecord<T>
{
    /// <summary>
    /// The Bivector
    /// </summary>
    RGaBivector<T> Bivector { get; }
}