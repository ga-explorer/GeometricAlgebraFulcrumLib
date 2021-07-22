using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D
{
    public static class GaEuclideanSpace2DUtils
    {
        public static GaProcessorGenericOrthonormal<double> MultivectorProcessor { get; }
            = GaProcessorGenericOrthonormal<double>.CreateEuclidean(
                GaScalarProcessorFloat64.DefaultProcessor,
                2
            );
    }
}