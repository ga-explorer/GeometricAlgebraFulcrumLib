namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators;

public sealed class DfFourierSignalInterpolatorOptions :
    DfSignalInterpolatorOptions
{
    public double EnergyAcThreshold { get; set; } = double.NegativeInfinity;

    public double EnergyAcPercentThreshold { get; set; } = 1d;

    public double SignalToNoiseRatioThreshold { get; set; } = double.PositiveInfinity;

    public double FrequencyThreshold { get; set; } = double.PositiveInfinity;

    public int FrequencyCountThreshold { get; set; } = int.MaxValue;

    public int PaddingPolynomialDegree { get; set; } = 6;

    public int PaddingTrendSampleCount { get; set; } = 1;

    public int PaddingSampleCount { get; set; } = -1;

    public bool AssumePeriodic { get; set; } = false;
}