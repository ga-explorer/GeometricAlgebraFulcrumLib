using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaMultivector<T> Multivector { get; }
}