using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

/// <summary>
/// https://en.wikipedia.org/wiki/Non-analytic_smooth_function#Smooth_transition_functions
/// https://www.youtube.com/watch?v=vD5g8aVscUI
/// </summary>
public sealed class Float64ScalarSmoothBlendSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSmoothBlendSignal Finite(double blendTimeMin, double blendTimeMax, Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
    {
        return new Float64ScalarSmoothBlendSignal(
            Float64ScalarRange.Create(blendTimeMin, blendTimeMax),
            false, 
            baseSignal1, 
            baseSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSmoothBlendSignal Finite(Float64ScalarRange timeRange, Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
    {
        return new Float64ScalarSmoothBlendSignal(
            timeRange,
            false, 
            baseSignal1, 
            baseSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSmoothBlendSignal Periodic(Float64ScalarRange timeRange, Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
    {
        return new Float64ScalarSmoothBlendSignal(
            timeRange,
            true, 
            baseSignal1, 
            baseSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSmoothBlendSignal Periodic(double blendTimeMin, double blendTimeMax, Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
    {
        return new Float64ScalarSmoothBlendSignal(
            Float64ScalarRange.Create(blendTimeMin, blendTimeMax),
            true, 
            baseSignal1, 
            baseSignal2
        );
    }


    public Float64ScalarSignal BaseSignal1 { get; }

    public Float64ScalarSignal BaseSignal2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSmoothBlendSignal(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
        : base(timeRange, isPeriodic)
    {
        BaseSignal1 = baseSignal1;
        BaseSignal2 = baseSignal2;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal1.IsValid() &&
               BaseSignal2.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarSmoothBlendSignal(
                TimeRange,
                false,
                BaseSignal1,
                BaseSignal2
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarSmoothBlendSignal(
                TimeRange,
                true,
                BaseSignal1,
                BaseSignal2
            );
    }


    private double RampFunction(double t)
    {
        Debug.Assert(
            t >= MinTime && t <= MaxTime
        );

        //if (t <= TimeMin) return 0;
        //if (t >= TimeMax) return 1;

        var y = (t - MinTime) / (MaxTime - MinTime);

        Debug.Assert(y is >= 0 and <= 1);

        return y;
    }

    private double SmoothUnitStepFunction(double t)
    {
        Debug.Assert(
            t >= MinTime && t <= MaxTime
        );

        //if (t <= BlendTimeMin) return 0;
        //if (t >= BlendTimeMax) return 1;

        t = (t - MinTime) / (MaxTime - MinTime);

        var s = 1 - t;
        var x = 1 / t - 1 / s;

        var y = 1 / (1 + Math.Exp(x));

        //var e1 = Math.Exp(-1d / t);
        //var e2 = Math.Exp(-1d / (1d - t));

        //var y = e1 / (e1 + e2);

        Debug.Assert(y is >= 0 and <= 1);

        return y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        //var x = RampFunction(t);
        var x = SmoothUnitStepFunction(t);
        var y = 1d - x;

        return BaseSignal1.GetValue(t) * y + BaseSignal2.GetValue(t) * x;
    }

}