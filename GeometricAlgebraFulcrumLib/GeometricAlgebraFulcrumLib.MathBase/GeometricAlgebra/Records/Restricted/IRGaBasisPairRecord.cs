using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public interface IRGaBasisPairRecord
{
    /// <summary>
    /// The First Basis Blade
    /// </summary>
    RGaBasisBlade BasisBlade1 { get; }

    /// <summary>
    /// The Second Basis Blade
    /// </summary>
    RGaBasisBlade BasisBlade2 { get; }
}