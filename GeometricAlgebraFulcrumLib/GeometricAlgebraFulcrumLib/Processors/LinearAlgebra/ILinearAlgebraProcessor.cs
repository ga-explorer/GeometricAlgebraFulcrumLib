using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public interface ILinearAlgebraProcessor<T>
        : IScalarAlgebraProcessor<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        //T LinDotProduct(ILinVectorStorage<T> v1, ILinVectorStorage<T> v2);
    }
}