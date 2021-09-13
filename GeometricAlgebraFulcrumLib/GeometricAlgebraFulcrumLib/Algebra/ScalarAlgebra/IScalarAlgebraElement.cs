using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra
{
    public interface IScalarAlgebraElement<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }
    }
}