using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

public sealed class Float64ScalarSimpleHarmonicSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSimpleHarmonicSignal Finite(int harmonicFactor, double magnitude, double timeOffset = 0)
    {
        return new Float64ScalarSimpleHarmonicSignal(false, harmonicFactor, magnitude, timeOffset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSimpleHarmonicSignal Periodic(int harmonicFactor, double magnitude, double timeOffset = 0)
    {
        return new Float64ScalarSimpleHarmonicSignal(true, harmonicFactor, magnitude, timeOffset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSimpleHarmonicSignal Create(bool isPeriodic, int harmonicFactor, double magnitude, double timeOffset = 0)
    {
        return new Float64ScalarSimpleHarmonicSignal(isPeriodic, harmonicFactor, magnitude, timeOffset);
    }


    public int HarmonicFactor { get; }

    public double Magnitude { get; }

    public double TimeOffset { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSimpleHarmonicSignal(bool isPeriodic, int harmonicFactor, double magnitude, double timeOffset = 0d)
        : base(Float64ScalarRange.SymmetricPi, isPeriodic)
    {
        //if (harmonicFactor < 1)
        //    throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

        if (!magnitude.IsFinite())
            throw new ArgumentException(nameof(magnitude));

        if (!timeOffset.IsFinite())
            throw new ArgumentException(nameof(timeOffset));

        HarmonicFactor = harmonicFactor;
        Magnitude = magnitude;
        TimeOffset = timeOffset;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Magnitude.IsFinite() &&
               TimeOffset.IsFinite();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarSimpleHarmonicSignal(
                false,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarSimpleHarmonicSignal(
                true,
                HarmonicFactor,
                Magnitude,
                TimeOffset
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return Magnitude * (w * (t + TimeOffset)).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;

        return -Magnitude * w * (w * (t + TimeOffset)).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        t = this.ClampTime(t);

        var w = Math.Tau * HarmonicFactor;
        var w2 = w * w;

        return -Magnitude * w2 * (w * (t + TimeOffset)).Cos();
    }


}