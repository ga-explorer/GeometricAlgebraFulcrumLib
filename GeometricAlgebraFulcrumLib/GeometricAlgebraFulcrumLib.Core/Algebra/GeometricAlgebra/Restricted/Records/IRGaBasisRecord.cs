using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisRecord
{
    /// <summary>
    /// The Basis Blade
    /// </summary>
    RGaBasisBlade BasisBlade { get; }
}