using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public interface IGeometricAlgebraProcessor<T> : 
        IGeometricAlgebraSpace, 
        ILinearAlgebraProcessor<T>
    {
        IGeometricAlgebraSignature Signature { get; }

        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        KVectorStorage<T> PseudoScalar { get; }

        KVectorStorage<T> PseudoScalarInverse { get; }

        KVectorStorage<T> PseudoScalarReverse { get; }

        IMultivectorStorage<T> Normalize(IMultivectorStorage<T> mv1);
    }
}