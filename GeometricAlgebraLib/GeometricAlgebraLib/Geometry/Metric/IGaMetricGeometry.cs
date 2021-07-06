using GeometricAlgebraLib.Processing.Multivectors;

namespace GeometricAlgebraLib.Geometry.Metric
{
    public interface IGaMetricGeometry<T> : 
        IGaGeometricElement<T>
    {
        IGaMultivectorProcessor<T> MultivectorProcessor { get; }
    }
}