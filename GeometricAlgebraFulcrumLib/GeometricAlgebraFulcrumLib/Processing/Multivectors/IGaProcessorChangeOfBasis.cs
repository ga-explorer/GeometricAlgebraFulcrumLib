using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public interface IGaProcessorChangeOfBasis<T> :
        IGaProcessor<T>
    {
        /// <summary>
        /// The outermorphism that maps multivectors from this target basis to
        /// the source orthonormal basis
        /// </summary>
        IGaOutermorphism<T> OmTargetToOrthonormal { get; }

        /// <summary>
        /// The outermorphism that maps multivectors from the source orthonormal
        /// basis to this target basis
        /// </summary>
        IGaOutermorphism<T> OmOrthonormalToTarget { get; }
    }
}