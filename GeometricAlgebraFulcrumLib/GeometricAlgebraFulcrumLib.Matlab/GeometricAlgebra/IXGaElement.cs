namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

public interface IXGaElement : 
    IAlgebraicElement
{
    XGaMetric Metric { get; }
}