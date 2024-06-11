using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

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