using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public interface IXGaMultivectorRecord
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaFloat64Multivector Multivector { get; }
}

public interface IXGaMultivectorRecord<T>
{
    /// <summary>
    /// The Multivector
    /// </summary>
    XGaMultivector<T> Multivector { get; }
}