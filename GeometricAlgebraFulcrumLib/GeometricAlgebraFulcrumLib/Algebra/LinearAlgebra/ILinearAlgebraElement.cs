using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra
{
    public interface ILinearAlgebraElement<T> :
        IScalarAlgebraElement<T>
    {
        ILinearProcessor<T> LinearProcessor { get; }
    }
}