using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public interface ILinearAlgebraProcessor<T>
        : IScalarAlgebraProcessor<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }
    }
}