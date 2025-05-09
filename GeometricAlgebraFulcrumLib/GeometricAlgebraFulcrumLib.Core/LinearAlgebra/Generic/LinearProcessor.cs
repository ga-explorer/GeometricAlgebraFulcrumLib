using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic;

public class LinearProcessor<T> :
    ScalarProcessorContainer<T>,
    ILinearProcessor<T>
{
    internal LinearProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor)
    {
    }
}