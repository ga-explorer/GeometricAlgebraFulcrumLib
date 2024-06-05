using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;

public class LinearProcessor<T> :
    ScalarProcessorContainer<T>,
    ILinearProcessor<T>
{
    internal LinearProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor)
    {
    }
}