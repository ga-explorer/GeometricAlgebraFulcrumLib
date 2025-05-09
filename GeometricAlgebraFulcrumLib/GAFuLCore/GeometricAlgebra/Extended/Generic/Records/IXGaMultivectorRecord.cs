using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaMultivector<T> Multivector { get; }
}