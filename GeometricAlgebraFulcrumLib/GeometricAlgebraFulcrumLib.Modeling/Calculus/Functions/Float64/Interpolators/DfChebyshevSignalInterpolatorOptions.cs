namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public sealed class DfChebyshevSignalInterpolatorOptions :
    DfSignalInterpolatorOptions
{
    public int PolynomialDegree { get; set; } = 39;
}