using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public interface IGeometricAlgebraProcessor<T> : 
        IGeometricAlgebraSpace, 
        ILinearAlgebraProcessor<T>
    {
        GeometricAlgebraBasisSet BasisSet { get; }

        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        KVectorStorage<T> PseudoScalar { get; }

        KVectorStorage<T> PseudoScalarInverse { get; }

        KVectorStorage<T> PseudoScalarReverse { get; }

        IMultivectorStorage<T> Normalize(IMultivectorStorage<T> mv1);
    }
}