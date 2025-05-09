using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

public class RGaEuclideanProcessor<T> :
    RGaProcessor<T>
{
    internal RGaEuclideanProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 0, 0)
    {
    }
}