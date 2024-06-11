using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisRecord
{
    /// <summary>
    /// The Basis Blade
    /// </summary>
    RGaBasisBlade BasisBlade { get; }
}