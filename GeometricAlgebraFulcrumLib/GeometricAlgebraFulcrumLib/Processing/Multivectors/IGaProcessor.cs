using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public interface IGaProcessor<T> : 
        IGaSpace, IGaScalarsGridProcessor<T>
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