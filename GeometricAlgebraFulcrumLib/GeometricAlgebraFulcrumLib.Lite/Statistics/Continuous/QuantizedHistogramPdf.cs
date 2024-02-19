namespace GeometricAlgebraFulcrumLib.Lite.Statistics.Continuous;

public class QuantizedHistogramPdf :
    Random
{
    public Random UniformRandomGenerator { get; }

    public QuantizedHistogram BaseHistogram { get; }

    public PiecewiseAffineFunction InverseDistributionFunction { get; }

    
    public QuantizedHistogramPdf(QuantizedHistogram baseHistogram)
    {
        UniformRandomGenerator = new Random();
        BaseHistogram = baseHistogram;
        InverseDistributionFunction = baseHistogram.GetInverseDistributionFunction();
    }

    public QuantizedHistogramPdf(QuantizedHistogram baseHistogram, int seed)
    {
        UniformRandomGenerator = new Random(seed);
        BaseHistogram = baseHistogram;
        InverseDistributionFunction = baseHistogram.GetInverseDistributionFunction();
    }


    public override double NextDouble()
    {
        return InverseDistributionFunction.GetValue(
            UniformRandomGenerator.NextDouble()
        );
    }
}