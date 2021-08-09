using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space2D
{
    public static class GaEuclideanSpace2DUtils
    {
        public static IGaProcessorEuclidean<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(2);
    }
}