namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra;

public interface IXGaElement : 
    IAlgebraicElement
{
    XGaMetric Metric { get; }
}