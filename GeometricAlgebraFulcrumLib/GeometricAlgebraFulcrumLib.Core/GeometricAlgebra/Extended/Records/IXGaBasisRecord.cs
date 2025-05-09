using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public interface IXGaBasisRecord
{
    /// <summary>
    /// The Basis Blade
    /// </summary>
    XGaBasisBlade BasisBlade { get; }
}