namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;

public interface IRGaElement : 
    IAlgebraicElement
{
    RGaMetric Metric { get; }
}