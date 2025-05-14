using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Records;

public interface IXGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaMultivector<T> Multivector { get; }
}