using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Records;

public interface IXGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaMultivector<T> Multivector { get; }
}