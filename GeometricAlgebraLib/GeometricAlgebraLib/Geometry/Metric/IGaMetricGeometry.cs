using GeometricAlgebraLib.Processors.Multivectors;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public interface IGaMetricGeometry<T> : IGaGeometricElement<T>
    {
        GaMultivectorsProcessor<T> Processor { get; }
    }
}