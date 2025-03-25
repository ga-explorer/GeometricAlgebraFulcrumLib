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
        => Math.Tau * BaseCycleFrequencyHz;

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

    public Float64SampledTimeSignal[] GenerateEvenSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
    {
        var phi = Math.Tau / phaseCount;
        var scalarSignalArray = new Float64SampledTimeSignal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64SampledTimeSignal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos()
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64SampledTimeSignal[] GenerateEvenSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
    {
        var phi = Math.Tau / phaseCount;
        var scalarSignalArray = new Float64SampledTimeSignal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64SampledTimeSignal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Cos()
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64SampledTimeSignal[] GenerateOddSignalComponents(double magnitude, double harmonicFactor, int phaseCount)
    {
        var phi = Math.Tau / phaseCount;
        var scalarSignalArray = new Float64SampledTimeSignal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64SampledTimeSignal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitude * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin()
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

    public Float64SampledTimeSignal[] GenerateOddSignalComponents(IReadOnlyList<double> magnitudeList, double harmonicFactor, int phaseCount)
    {
        var phi = Math.Tau / phaseCount;
        var scalarSignalArray = new Float64SampledTimeSignal[phaseCount];

        for (var phaseIndex = 0; phaseIndex < phaseCount; phaseIndex++)
        {
            var k = phaseIndex;

            scalarSignalArray[phaseIndex] = Float64SampledTimeSignal.CreatePeriodic(
                BaseCycleSampleCount,
                BaseCycleTime,
                t =>
                    magnitudeList[k] * (harmonicFactor * (BaseCycleFrequency * t - k * phi)).Sin()
            ).Repeat(BaseCycleCount);
        }

        return scalarSignalArray;
    }

}