namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;

public interface IXGaElement : 
    IAlgebraicElement
{
    XGaMetric Metric { get; }
}