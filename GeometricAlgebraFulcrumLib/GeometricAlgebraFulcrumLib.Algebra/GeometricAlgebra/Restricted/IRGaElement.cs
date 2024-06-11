namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted;

public interface IRGaElement : 
    IAlgebraicElement
{
    RGaMetric Metric { get; }
}