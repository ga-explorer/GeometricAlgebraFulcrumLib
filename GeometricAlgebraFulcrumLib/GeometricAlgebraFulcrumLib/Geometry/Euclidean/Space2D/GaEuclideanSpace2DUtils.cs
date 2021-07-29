using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D
{
    public static class GaEuclideanSpace2DUtils
    {
        public static IGaProcessorEuclidean<double> MultivectorProcessor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(2);
    }
}