namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted;

public interface IRGaElement : 
    IAlgebraicElement
{
    RGaMetric Metric { get; }
}