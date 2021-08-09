using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space3D
{
    public static class GaEuclideanSpace3DUtils
    {
        public static IGaProcessorEuclidean<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(3);
    }
}