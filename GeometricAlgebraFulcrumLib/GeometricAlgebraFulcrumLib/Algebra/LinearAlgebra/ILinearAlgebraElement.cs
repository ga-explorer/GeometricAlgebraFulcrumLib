using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra
{
    public interface ILinearAlgebraElement<T> :
        IScalarAlgebraElement<T>
    {
        ILinearAlgebraProcessor<T> LinearProcessor { get; }
    }
}