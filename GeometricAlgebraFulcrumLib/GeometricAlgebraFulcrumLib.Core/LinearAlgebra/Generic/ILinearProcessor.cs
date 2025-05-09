using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic;

public interface ILinearProcessor<T>
    : IScalarProcessor<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    //T LinDotProduct(ILinVectorStorage<T> v1, ILinVectorStorage<T> v2);
}