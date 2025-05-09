using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaMultivector<T> Multivector { get; }
}