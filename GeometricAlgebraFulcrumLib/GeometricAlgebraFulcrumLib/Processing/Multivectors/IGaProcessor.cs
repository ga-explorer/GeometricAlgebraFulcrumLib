using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public interface IGaProcessor<T> : 
        IGaSpace, IGaScalarProcessor<T>
    {
        IGaSignature Signature { get; }
        
        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        IGaStorageKVector<T> PseudoScalar { get; }

        IGaStorageKVector<T> PseudoScalarInverse { get; }

        IGaStorageKVector<T> PseudoScalarReverse { get; }

        IGaStorageMultivector<T> Normalize(IGaStorageMultivector<T> mv1);
    }
}