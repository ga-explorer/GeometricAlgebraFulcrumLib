using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted
{
    public interface IRGaBasisRecord
    {
        /// <summary>
        /// The Basis Blade
        /// </summary>
        RGaBasisBlade BasisBlade { get; }
    }
}