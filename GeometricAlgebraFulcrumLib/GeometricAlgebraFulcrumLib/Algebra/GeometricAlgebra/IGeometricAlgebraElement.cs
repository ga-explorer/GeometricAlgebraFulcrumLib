using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra
{
    public interface IGeometricAlgebraElement<T> :
        ILinearAlgebraElement<T>
    {
        IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
    }
}