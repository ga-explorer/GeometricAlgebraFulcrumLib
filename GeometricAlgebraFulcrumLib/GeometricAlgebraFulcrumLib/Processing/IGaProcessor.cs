using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public interface IGaProcessor<T> : 
        IGaSpace, IGaScalarProcessor<T>
    {
        IGaSignature Signature { get; }
        
        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        IGasKVector<T> PseudoScalar { get; }

        IGasKVector<T> PseudoScalarInverse { get; }

        IGasKVector<T> PseudoScalarReverse { get; }

        IGasMultivector<T> Normalize(IGasMultivector<T> mv1);
    }
}