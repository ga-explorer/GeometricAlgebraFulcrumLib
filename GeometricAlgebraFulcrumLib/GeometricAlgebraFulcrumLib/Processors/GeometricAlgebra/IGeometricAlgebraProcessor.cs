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

        GaKVector<T> PseudoScalar { get; }

        GaKVector<T> PseudoScalarInverse { get; }

        GaKVector<T> PseudoScalarReverse { get; }
    }
}