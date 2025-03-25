using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarSegmentSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal Finite(Float64ScalarRange timeRange, Float64ScalarSignal baseSignal)
    {
        return new Float64ScalarSegmentSignal(timeRange, false, baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal Finite(double timeMin, double timeMax, Float64ScalarSignal baseSignal)
    {
        if (timeMin < timeMax)
            return new Float64ScalarSegmentSignal(
                Float64ScalarRange.Create(timeMin, timeMax), 
                false, 
                baseSignal
            );

        return new Float64ScalarSegmentSignal(
            Float64ScalarRange.Create(timeMax, timeMin), 
            false, 
            baseSignal.FlipTimeRange(timeMax, timeMin)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal FiniteFromRelativeTime(Float64ScalarSignal baseSignal, double t1, double t2)
    {
        var timeMin = baseSignal.GetRelativeTime(t1, false);
        var timeMax = baseSignal.GetRelativeTime(t2, false);

        return Finite(timeMin, timeMax, baseSignal);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal Periodic(Float64ScalarRange timeRange, Float64ScalarSignal baseSignal)
    {
        return new Float64ScalarSegmentSignal(timeRange, true, baseSignal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal Periodic(double timeMin, double timeMax, Float64ScalarSignal baseSignal)
    {
        if (timeMin < timeMax)
            return new Float64ScalarSegmentSignal(
                Float64ScalarRange.Create(timeMin, timeMax), 
                true, 
                baseSignal
            );

        return new Float64ScalarSegmentSignal(
            Float64ScalarRange.Create(timeMax, timeMin), 
            true, 
            baseSignal.FlipTimeRange(timeMax, timeMin)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSegmentSignal PeriodicFromRelativeTime(Float64ScalarSignal baseSignal, double t1, double t2)
    {
        var timeMin = baseSignal.GetRelativeTime(t1, false);
        var timeMax = baseSignal.GetRelativeTime(t2, false);

        return Periodic(timeMin, timeMax, baseSignal);
    }


    public Float64ScalarSignal BaseSignal { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSegmentSignal(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal baseSignal)
        : base(timeRange, isPeriodic)
    {
        BaseSignal = baseSignal;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarSegmentSignal(
                TimeRange,
                false,
                BaseSignal
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarSegmentSignal(
                TimeRange,
                true,
                BaseSignal
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return BaseSignal.GetValue(
            TimeRange.Clamp(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return BaseSignal.GetDerivative1Value(
            TimeRange.Clamp(t)
        );
    }
}