using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Records;

public interface IXGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaMultivector<T> Multivector { get; }
}