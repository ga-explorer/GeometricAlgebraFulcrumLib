using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;

public interface ILinearProcessor<T>
    : IScalarProcessor<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    //T LinDotProduct(ILinVectorStorage<T> v1, ILinVectorStorage<T> v2);
}