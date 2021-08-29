using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaEuclideanSpace3DUtils
    {
        public static IGaProcessorEuclidean<double> Processor { get; }
            = Float64ScalarProcessor.DefaultProcessor.CreateGaEuclideanProcessor(3);
    }
}