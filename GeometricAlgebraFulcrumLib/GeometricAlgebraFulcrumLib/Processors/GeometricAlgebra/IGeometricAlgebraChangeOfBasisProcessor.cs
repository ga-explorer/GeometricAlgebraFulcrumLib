using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public interface IGeometricAlgebraChangeOfBasisProcessor<T> :
        IGeometricAlgebraProcessor<T>
    {
        /// <summary>
        /// The outermorphism that maps multivectors from this target basis to
        /// the source orthonormal basis
        /// </summary>
        IOutermorphism<T> OmTargetToOrthonormal { get; }

        /// <summary>
        /// The outermorphism that maps multivectors from the source orthonormal
        /// basis to this target basis
        /// </summary>
        IOutermorphism<T> OmOrthonormalToTarget { get; }
    }
}