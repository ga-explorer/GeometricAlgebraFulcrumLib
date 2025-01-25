using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;

public class Float64HarmonicSignalComposer
{
    public static Float64HarmonicSignalComposer Create()
    {
        return new Float64HarmonicSignalComposer();
    }

    public static Float64HarmonicSignalComposer Create(double baseCycleFrequencyHz, int baseCycleSampleCount, int baseCycleCount)
    {
        return new Float64HarmonicSignalComposer()
        {
            BaseCycleFrequencyHz = baseCycleFrequencyHz,
            BaseCycleSampleCount = baseCycleSampleCount,
            BaseCycleCount = baseCycleCount
        };
    }


    public int BaseCycleSampleCount { get; set; } = 1000;

    public int BaseCycleCount { get; set; } = 10;

    public double BaseCycleFrequencyHz { get; set; } = 50;

    public double BaseCycleFrequency
        => 2 * Math.PI * BaseCycleFrequencyHz;

    public double BaseCycleTime
        => 1d / BaseCycleFrequencyHz;

    public int SampleCount
        => BaseCycleCount * BaseCycleSampleCount;

    public double SamplingRate
        => BaseCycleSampleCount * BaseCycleFrequencyHz;


    private Float64HarmonicSignalComposer()
    {
    }


    public Float64SamplingSpecs GetSamplingSpecs()
    {
        return Float64SamplingSpecs.CreateFromSamplingRate(SampleCount, SamplingRate);
    }

    public Float64Signal[] GenerateEvenSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
    {
        var phi = 2d * Math.PI / phaseCount;
        var scalarSignalArray = new Float64Signal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64Signal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos(),
                false
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64Signal[] GenerateEvenSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
    {
        var phi = 2d * Math.PI / phaseCount;
        var scalarSignalArray = new Float64Signal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64Signal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos(),
                false
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64Signal[] GenerateOddSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
    {
        var phi = 2d * Math.PI / phaseCount;
        var scalarSignalArray = new Float64Signal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64Signal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin(),
                false
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64Signal[] GenerateOddSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
    {
        var phi = 2d * Math.PI / phaseCount;
        var scalarSignalArray = new Float64Signal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64Signal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin(),
                false
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

}