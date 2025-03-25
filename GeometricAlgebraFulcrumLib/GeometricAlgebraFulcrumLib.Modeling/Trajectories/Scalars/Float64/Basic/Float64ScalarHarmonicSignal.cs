using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

public class Float64ScalarHarmonicSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarHarmonicSignal Finite(Float64ScalarRange timeRange, double frequencyHz = 1d, double magnitude = 1, double timeOffset = 0)
    {
        return new Float64ScalarHarmonicSignal(
            timeRange,
            false,
            frequencyHz,
            magnitude,
            timeOffset
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarHarmonicSignal Periodic(Float64ScalarRange timeRange, double frequencyHz = 1d, double magnitude = 1, double timeOffset = 0)
    {
        return new Float64ScalarHarmonicSignal(
            timeRange,
            true,
            frequencyHz,
            magnitude,
            timeOffset
        );
    }


    public double FrequencyHz { get; }

    public double Frequency
        => Math.Tau * FrequencyHz;

    public double Magnitude { get; }

    public double TimeOffset { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarHarmonicSignal(Float64ScalarRange timeRange, bool isPeriodic, double frequencyHz, double magnitude, double timeOffset)
        : base(timeRange, isPeriodic)
    {
        FrequencyHz = frequencyHz;
        Magnitude = magnitude;
        TimeOffset = timeOffset;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Magnitude.IsFinite() &&
               TimeOffset.IsFinite() &&
               FrequencyHz.IsFinite();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        return Magnitude * (Frequency * (t + TimeOffset)).Cos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarHarmonicSignal(
                TimeRange, 
                false,
                FrequencyHz,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarHarmonicSignal(
                TimeRange, 
                true,
                FrequencyHz,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        var w = Frequency;

        return -Magnitude * w * (w * (t + TimeOffset)).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        var w = Frequency;

        return -Magnitude * w * w * (w * (t + TimeOffset)).Cos();
    }

}