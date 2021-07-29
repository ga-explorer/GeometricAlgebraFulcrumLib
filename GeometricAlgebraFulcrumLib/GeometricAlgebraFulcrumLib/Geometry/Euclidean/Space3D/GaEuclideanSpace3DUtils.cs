using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D
{
    public static class GaEuclideanSpace3DUtils
    {
        public static IGaProcessorEuclidean<double> MultivectorProcessor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(3);
    }
}