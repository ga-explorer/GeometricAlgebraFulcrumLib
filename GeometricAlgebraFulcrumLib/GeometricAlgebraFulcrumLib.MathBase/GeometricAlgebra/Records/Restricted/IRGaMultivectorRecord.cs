using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public interface IRGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaFloat64Multivector Multivector { get; }
}

public interface IRGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    RGaMultivector<T> Multivector { get; }
}