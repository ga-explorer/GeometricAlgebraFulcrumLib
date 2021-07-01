using GeometricAlgebraLib.Processors.Multivectors;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public interface IGaMetricGeometry<T> : 
        IGaGeometricElement<T>
    {
        IGaMultivectorsProcessor<T> MultivectorProcessor { get; }
    }
}