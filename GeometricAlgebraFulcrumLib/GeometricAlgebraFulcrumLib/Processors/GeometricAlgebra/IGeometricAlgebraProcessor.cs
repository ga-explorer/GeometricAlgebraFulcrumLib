using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public interface IGeometricAlgebraProcessor<T> : 
        IGeometricAlgebraSpace, 
        ILinearAlgebraProcessor<T>
    {
        BasisBladeSet BasisSet { get; }

        bool IsOrthonormal { get; }

        bool IsChangeOfBasis { get; }

        bool IsOrthonormalEuclidean { get; }

        KVector<T> PseudoScalar { get; }

        KVector<T> PseudoScalarInverse { get; }

        KVector<T> PseudoScalarReverse { get; }
    }
}