using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public interface IRGaBasisRecord
{
    /// <summary>
    /// The Basis Blade
    /// </summary>
    RGaBasisBlade BasisBlade { get; }
}