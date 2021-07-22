using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D
{
    public static class GaEuclideanSpace3DUtils
    {
        public static GaProcessorGenericOrthonormal<double> MultivectorProcessor { get; }
            = GaProcessorGenericOrthonormal<double>.CreateEuclidean(
                GaScalarProcessorFloat64.DefaultProcessor,
                3
            );
    }
}