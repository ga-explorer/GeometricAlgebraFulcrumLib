using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public interface IRGaVectorRecord
{
    /// <summary>
    /// The Vector
    /// </summary>
    RGaFloat64Vector Vector { get; }
}

public interface IRGaVectorRecord<T>
{
    /// <summary>
    /// The Vector
    /// </summary>
    RGaVector<T> Vector { get; }
}
