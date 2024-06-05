using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public interface IXGaBasisRecord
{
    /// <summary>
    /// The Basis Blade
    /// </summary>
    XGaBasisBlade BasisBlade { get; }
}