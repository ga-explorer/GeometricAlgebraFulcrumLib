namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;

public interface IRGaElement : 
    IGeometricElement
{
    RGaMetric Metric { get; }
}