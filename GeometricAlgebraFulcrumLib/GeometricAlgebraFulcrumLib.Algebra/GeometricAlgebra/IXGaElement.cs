namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;

public interface IXGaElement : 
    IAlgebraicElement
{
    XGaMetric Metric { get; }
}