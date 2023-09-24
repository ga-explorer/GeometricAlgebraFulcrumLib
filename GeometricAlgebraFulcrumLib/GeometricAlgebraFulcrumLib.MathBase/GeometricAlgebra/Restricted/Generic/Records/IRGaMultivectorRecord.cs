using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;

public interface IRGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaMultivector<T> Multivector { get; }
}