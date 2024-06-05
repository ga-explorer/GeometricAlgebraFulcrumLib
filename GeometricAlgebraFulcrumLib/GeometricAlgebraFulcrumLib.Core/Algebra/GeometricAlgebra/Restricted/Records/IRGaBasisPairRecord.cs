using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

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