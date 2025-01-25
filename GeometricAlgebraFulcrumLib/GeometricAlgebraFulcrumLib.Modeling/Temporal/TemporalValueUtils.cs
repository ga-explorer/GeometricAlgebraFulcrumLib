using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal;

public static class TemporalValueUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsTime(this ITemporalValue value, double t)
    {
        return value.TimeRange.Contains(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TimeClamp(this ITemporalValue value, double t, bool clampPeriodic = false)
    {
        return clampPeriodic
            ? t.ClampPeriodic(value.MinTime, value.MaxTime)
            : t.Clamp(value.MinTime, value.MaxTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TimeClamp(this ITemporalValue value, Float64Scalar t, bool clampPeriodic = false)
    {
        return value.TimeClamp(t.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TimeClamp(this ITemporalValue value, IFloat64Scalar t, bool clampPeriodic = false)
    {
        return value.TimeClamp(t.ScalarValue, clampPeriodic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetRelativeTime(this ITemporalValue value, double t, bool clampPeriodic = false)
    {
        t = value.TimeClamp(t, clampPeriodic);

        return ((t - value.MinTime) / (value.MaxTime - value.MinTime)).ClampToUnit();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetRelativeTime(this ITemporalValue value, Float64Scalar t, bool clampPeriodic = false)
    {
        return value.GetRelativeTime(t.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetRelativeTime(this ITemporalValue value, IFloat64Scalar t, bool clampPeriodic = false)
    {
        return value.GetRelativeTime(t.ScalarValue, clampPeriodic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetTime(this ITemporalValue value, double tLerp, bool clampPeriodic = false)
    {
        var x = clampPeriodic 
            ? tLerp.ClampPeriodicToUnit() 
            : tLerp.ClampToUnit();

        return value.TimeClamp(
            (1 - x) * value.MinTime + x * value.MaxTime
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetTime(this ITemporalValue value, Float64Scalar tLerp, bool clampPeriodic = false)
    {
        return value.GetTime(tLerp.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetTime(this ITemporalValue value, IFloat64Scalar tLerp, bool clampPeriodic = false)
    {
        return value.GetTime(tLerp.ScalarValue, clampPeriodic);
    }


}