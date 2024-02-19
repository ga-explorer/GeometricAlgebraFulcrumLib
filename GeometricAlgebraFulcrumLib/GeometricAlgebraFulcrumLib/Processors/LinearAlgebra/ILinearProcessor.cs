using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;

public interface ILinearProcessor<T>
    : IScalarProcessor<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    //T LinDotProduct(ILinVectorStorage<T> v1, ILinVectorStorage<T> v2);
}