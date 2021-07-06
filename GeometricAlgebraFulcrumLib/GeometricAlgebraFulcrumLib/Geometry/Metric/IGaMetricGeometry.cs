using GeometricAlgebraFulcrumLib.Processing.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Metric
{
    public interface IGaMetricGeometry<T> : 
        IGaGeometricElement<T>
    {
        IGaMultivectorProcessor<T> MultivectorProcessor { get; }
    }
}