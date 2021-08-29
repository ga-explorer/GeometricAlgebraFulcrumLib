using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public interface IGaProcessor<T> : 
        IGaSpace, ILaProcessor<T>
    {
        IGaSignature Signature { get; }
        
        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        IGaKVectorStorage<T> PseudoScalar { get; }

        IGaKVectorStorage<T> PseudoScalarInverse { get; }

        IGaKVectorStorage<T> PseudoScalarReverse { get; }

        IGaMultivectorStorage<T> Normalize(IGaMultivectorStorage<T> mv1);
    }
}