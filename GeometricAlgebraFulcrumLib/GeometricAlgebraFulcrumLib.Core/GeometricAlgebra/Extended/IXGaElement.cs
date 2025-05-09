namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended;

public interface IXGaElement : 
    IAlgebraicElement
{
    XGaMetric Metric { get; }
}