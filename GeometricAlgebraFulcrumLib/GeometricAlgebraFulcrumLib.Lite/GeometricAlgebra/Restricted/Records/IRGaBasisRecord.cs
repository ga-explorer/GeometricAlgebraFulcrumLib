using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records
{
    public interface IRGaBasisRecord
    {
        /// <summary>
        /// The Basis Blade
        /// </summary>
        RGaBasisBlade BasisBlade { get; }
    }
}