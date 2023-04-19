using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public interface IXGaBasisPairRecord
{
    /// <summary>
    /// The First Basis Blade
    /// </summary>
    XGaBasisBlade BasisBlade1 { get; }

    /// <summary>
    /// The Second Basis Blade
    /// </summary>
    XGaBasisBlade BasisBlade2 { get; }
}