using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

public class XGaEuclideanProcessor<T> :
    XGaProcessor<T>
{
    internal XGaEuclideanProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 0, 0)
    {
    }
}